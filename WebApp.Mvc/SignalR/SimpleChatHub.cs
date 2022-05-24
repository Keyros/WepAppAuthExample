using Microsoft.AspNetCore.SignalR;

namespace WebApp.Mvc.SignalR;

public class SimpleChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User.Identity.Name;
        await Clients.Others.SendAsync("ReceiveMessage", userName, "I - connected");
        await base.OnConnectedAsync();
    }
}