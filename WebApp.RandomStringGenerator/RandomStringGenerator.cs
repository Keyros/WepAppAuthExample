namespace WebApp.RandomStringGenerator;

public class RandomStringGenerator
{
    private readonly Random _random = new();

    public string GenerateNextString()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, _random.Next(1, 10))
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}