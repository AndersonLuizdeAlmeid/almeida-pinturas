using MongoDB.Driver;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Documents.Infrastructure.Domain;
using System.Text;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using System.Threading;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IMongoCollection<Folder> _folderCollection;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQConsumer(IMongoDatabase database)
    {
        try
        {
            Console.WriteLine("[RabbitMQConsumer] Antes de tudo!");

            _folderCollection = database.GetCollection<Folder>("Folders");

            var factory = new ConnectionFactory()
            {
                HostName = "root-rabbitmq-1",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "UserCreatedQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine("[RabbitMQConsumer] Conexão com RabbitMQ criada!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("[RabbitMQConsumer] Falha ao conectar no RabbitMQ: " + ex.Message);
            throw;
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[RabbitMQConsumer] Iniciando ExecuteAsync...");

        try
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var user = JsonSerializer.Deserialize<Folder>(message);

                    Console.WriteLine($"[Consumer] Received message: {message}");

                    if (user != null)
                    {
                        CreateUserFolder(user);
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Consumer] Error processing message: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(queue: "UserCreatedQueue", autoAck: false, consumer: consumer);

            Console.WriteLine("[RabbitMQConsumer] Consumindo mensagens...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RabbitMQConsumer] Erro no ExecuteAsync: {ex.Message}");
        }

        return Task.CompletedTask;
    }

    private void CreateUserFolder(Folder folder)
    {
        var newFolder = new Folder
        {
            UserId = folder.UserId,
            FolderName = $"user_{folder.UserId}",
        };

        _folderCollection.InsertOne(newFolder);
        Console.WriteLine($"[Consumer] Virtual folder created for user: {folder.UserId}");
    }

}