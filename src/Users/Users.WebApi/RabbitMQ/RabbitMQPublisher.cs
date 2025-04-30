using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Users.WebApi.RabbitMQ;
public class RabbitMQPublisher
{
    private readonly string _hostname = "rabbitmq"; // Alterar se necessário
    private readonly string _queueName = "UserCreatedQueue";
    private readonly string _username;
    private readonly string _password;
    private readonly int _port;

    public RabbitMQPublisher(IConfiguration config)
    {
        _hostname = config["RabbitMq:Host"]!;
        _username = config["RabbitMq:Username"]!;
        _password = config["RabbitMq:Password"]!;
        _port = int.Parse(config["RabbitMq:Port"]!);
    }

    public void PublishUserCreatedEvent(long userId)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _username,
            Password = _password,
            Port = _port
        };


        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(new { UserId = userId });
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

            Console.WriteLine($"[x] Enviado evento para User ID: {userId}");
        }
    }

}