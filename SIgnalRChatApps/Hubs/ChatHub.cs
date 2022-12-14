using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
using System.Reflection;

namespace SIgnalRChatApps.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            try
            {
                HttpContext? httpContext = Context.GetHttpContext();

                /*
                 * define receiver and sender, so it can be collected to single hub pipe and both of them able to get message
                 */
                string? receiver = httpContext == null ? string.Empty : httpContext.Request.Query["userTarget"];
                string? sender = Context.User?.Identity?.Name;

                if (!string.IsNullOrEmpty(sender))
                    Groups.AddToGroupAsync(Context.ConnectionId, sender);

                if (!string.IsNullOrEmpty(receiver))
                    Groups.AddToGroupAsync(Context.ConnectionId, receiver);


                return base.OnConnectedAsync();
            }
            catch(Exception ex)
            {
                return base.OnDisconnectedAsync(ex);
            }
        }

        public Task SendMessageToGroup(string sender, string receiver, string message, string sentDate)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message, sentDate);
        }
    }
}
