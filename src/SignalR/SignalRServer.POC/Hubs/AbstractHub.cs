using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Entensions;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public abstract class AbstractHub<TMessageModel> : Hub
    {
        private readonly ConnectionMapping<string> _connections = new ConnectionMapping<string>();
        
        public async Task Broadcast(AbstractHubMessage<TMessageModel> message)
        {
            var messageJson = message.ToJson();
            await Clients.All.SendAsync("Receive", messageJson);
        }
        
        public async Task Send(AbstractHubMessage<TMessageModel> message)
        {
            var messageJson = message.ToJson();
            var connectionIds = _connections.GetConnections(message.From);
            
            foreach (var connectionId in connectionIds)
                await Clients.User(connectionId).SendAsync("Receive", messageJson);
        }

        public override Task OnConnectedAsync()
        {
            var userid = Context.GetContextHeaderValue("userid");
            _connections.Add(userid, Context.ConnectionId);
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userid = Context.GetContextHeaderValue("userid");
            _connections.Remove(userid, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}