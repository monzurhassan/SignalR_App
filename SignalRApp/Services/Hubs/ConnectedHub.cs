using Microsoft.AspNetCore.SignalR;

namespace SignalRApp.Services.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ConnectedHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConnectedHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task OnConnectedAsync()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(username))
            {
                ConnectedUsers.myConnectedUsers.Add(username);
                Clients.All.SendAsync("UpdateUsers", ConnectedUsers.myConnectedUsers);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(username))
            {
                ConnectedUsers.myConnectedUsers.Remove(username);
                Clients.All.SendAsync("UpdateUsers", ConnectedUsers.myConnectedUsers);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(username))
            {
               await Clients.All.SendAsync("ReceiveMessage", username, message);
            }
        }
    }

}
