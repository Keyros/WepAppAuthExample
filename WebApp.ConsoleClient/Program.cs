using WebApp.ConsoleClient;
using WebApp.ConsoleClient.Handlers;

var login = args.FirstOrDefault(x => x.Contains("login"))?.Split('=', StringSplitOptions.RemoveEmptyEntries).Last();
var path = args.FirstOrDefault(x => x.Contains("pass"))?.Split('=', StringSplitOptions.RemoveEmptyEntries).Last();

var tokesSource = new TokenStore();

var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
};
var refreshTokenHandler = new RefreshTokenHandler(httpClientHandler, tokesSource);
var authHandler = new AuthenticateHandler(refreshTokenHandler, login, path, tokesSource);
var httpClient = new HttpClient(authHandler);
httpClient.BaseAddress = new Uri("https://localhost:7014/");


await DoWork(1000);

async Task DoWork(int count)
{
    for (var i = 0; i < count; i++)
    {
        try
        {
            using var responseMessage = await httpClient.GetAsync("Home/Privacy/");
            Console.WriteLine(responseMessage.StatusCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        await Task.Delay(1000);
    }
}