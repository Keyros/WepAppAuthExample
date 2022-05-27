using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApp.RandomStringGenerator;

public class MessageConsumer : IDisposable
{
    private readonly string _queueName;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageConsumer(string hostName, string queueName, int port)
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

    public void Start()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };
        _channel.BasicConsume(queue: _queueName,
            autoAck: true,
            consumer: consumer);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}