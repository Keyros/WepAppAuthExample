namespace WebApp.RandomStringGenerator;

public class Message<T> : Message
{
    public Message(T body)
    {
        Body = body;
    }

    public T? Body { get; set; }
}

public abstract class Message
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime DateTime { get; } = DateTime.UtcNow;
    public static Message<T> Create<T>(T item) => new(item);
}