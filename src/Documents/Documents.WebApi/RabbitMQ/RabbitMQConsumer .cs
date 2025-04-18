﻿using MongoDB.Driver;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Documents.Infrastructure.Domain;
using System.Text;
using System.Text.Json;

public class RabbitMQConsumer : BackgroundService
{
    private readonly IMongoCollection<Folder> _folderCollection;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQConsumer(IMongoDatabase database)
    {
        _folderCollection = database.GetCollection<Folder>("Folders");

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "UserCreatedQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            if (stoppingToken.IsCancellationRequested)
                return;

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var user = JsonSerializer.Deserialize<Folder>(message);

            Console.WriteLine($"[Consumer] Received message: {message}");

            if (user != null)
            {
                CreateUserFolder(user);
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "UserCreatedQueue", autoAck: false, consumer: consumer);

        return Task.CompletedTask; // Deixa rodando até que o host seja encerrado
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