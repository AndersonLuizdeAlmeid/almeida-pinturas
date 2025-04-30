using Documents.Infrastructure.Domain;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IMongoCollection<Folder> _folderCollection;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMQConsumer> _logger;

    public RabbitMQConsumer(IMongoDatabase database, 
                            ILogger<RabbitMQConsumer> logger, 
                            IConfiguration config)
    {
        _logger = logger;
        try
        {
            _folderCollection = database.GetCollection<Folder>("Folders");

            _logger.LogInformation("[RabbitMQConsumer] Antes de tudo!");

            var factory = new ConnectionFactory()
            {
                HostName = config["RabbitMq:Host"],
                UserName = config["RabbitMq:Username"],
                Password = config["RabbitMq:Password"],
                Port = int.Parse(config["RabbitMq:Port"])
            };

            const int maxAttempts = 10;
            const int delayMs = 3000;
            int attempt = 0;
            while (true)
            {
                try
                {
                    attempt++;
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    _logger.LogInformation("[RabbitMQConsumer] Conectado no RabbitMQ na tentativa {Attempt}", attempt);
                    break;
                }
                catch (BrokerUnreachableException ex) when (attempt < maxAttempts)
                {
                    _logger.LogWarning(ex, "[RabbitMQConsumer] Tentativa {Attempt}/{Max} falhou, aguardando {Delay}ms",
                        attempt, maxAttempts, delayMs);
                    Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[RabbitMQConsumer] Não conseguiu conectar após {Attempt} tentativas", attempt);
                    throw;
                }
            }
            // ————————————

            _channel.QueueDeclare(
                queue: "UserCreatedQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            _logger.LogInformation("[RabbitMQConsumer] Fila declarada e pronta para consumir.");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RabbitMQConsumer] Falha ao conectar no RabbitMQ: {Message}", ex.Message);
            throw;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[RabbitMQConsumer] Iniciando consumo de mensagens...");

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("[RabbitMQConsumer] Cancelamento solicitado. Parando consumo de mensagens.");
                return;
            }

            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var user = JsonSerializer.Deserialize<Folder>(message);

                _logger.LogInformation("[RabbitMQConsumer] Mensagem recebida: {Message}", message);

                if (string.IsNullOrEmpty(message))
                {
                    _logger.LogWarning("[RabbitMQConsumer] Mensagem vazia recebida.");
                    return;
                }

                if (user != null)
                {
                    CreateUserFolder(user);
                }

                _channel.BasicAck(ea.DeliveryTag, multiple: false);
                _logger.LogInformation("[RabbitMQConsumer] Mensagem processada e confirmada.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RabbitMQConsumer] Erro ao processar mensagem: {Message}", ex.Message);
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _logger.LogInformation("[RabbitMQConsumer] Iniciando a escuta da fila...");
        _channel.BasicConsume(
            queue: "UserCreatedQueue",
            autoAck: false,
            consumer: consumer);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            // O host solicitou cancelamento, simplesmente encerra
        }
    }

    private void CreateUserFolder(Folder folder)
    {
        try
        {
            var newFolder = new Folder
            {
                UserId = folder.UserId,
                FolderName = $"user_{folder.UserId}"
            };

            _folderCollection.InsertOne(newFolder);
            _logger.LogInformation("[RabbitMQConsumer] Pasta virtual criada para o usuário: {UserId}", folder.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RabbitMQConsumer] Erro ao criar pasta: {Message}", ex.Message);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[RabbitMQConsumer] Parando consumidor e fechando conexões...");

        _channel?.Close();
        _connection?.Close();

        await base.StopAsync(cancellationToken);
    }
}
