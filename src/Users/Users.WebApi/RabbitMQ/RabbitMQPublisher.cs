using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Users.WebApi.RabbitMQ;
public class RabbitMQPublisher
{
    private readonly string _hostname = "rabbitmq"; // Alterar se necessário
    private readonly string _queueName = "UserCreatedQueue";

    public void PublishUserCreatedEvent(long userId, IConfiguration config)
    {
        var factory = new ConnectionFactory()
        {
            HostName = config["RabbitMq:Host"],
            UserName = config["RabbitMq:Username"],
            Password = config["RabbitMq:Password"],
            Port = int.Parse(config["RabbitMq:Port"])
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