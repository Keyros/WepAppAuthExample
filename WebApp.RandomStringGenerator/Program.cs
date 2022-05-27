// See https://aka.ms/new-console-template for more information

using WebApp.RandomStringGenerator;

Console.WriteLine("Hello, World!");
var consumer = new MessageConsumer("localhost", "test", 5672);
consumer.Start();

await RunProducer();


async Task RunProducer()
{
    var rndGenerator = new RandomStringGenerator();
    using (var messageProducer = new MessageProducer("localhost", "test", 5672))
    {
        for (int i = 0; i < 1000; i++)
        {
            messageProducer.SendMessage(
                Message.Create($"Привет {i}{Environment.NewLine}{rndGenerator.GenerateNextString()}"));
            await Task.Delay(1000);
        }
    }
}