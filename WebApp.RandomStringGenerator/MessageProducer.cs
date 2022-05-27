using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace WebApp.RandomStringGenerator;

public class MessageProducer : IDisposable
{
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageProducer(string hostName, string queueName, int port)
    {
        _queueName = queueName;
        var factory = new ConnectionFactory {HostName = hostName, Port = port, UserName = "admin", Password = "admin"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void SendMessage<T>(Message<T> message)
    {
        var body = JsonSerializer.Serialize(message);
        var queueMessage = Encoding.UTF8.GetBytes(body);

        _channel.BasicPublish(exchange: "",
            routingKey: _queueName,
            basicProperties: null,
            body: queueMessage);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}