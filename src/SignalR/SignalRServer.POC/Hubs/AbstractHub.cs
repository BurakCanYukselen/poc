using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Extensions;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public abstract class AbstractHub<TMessageModel> : Hub
    {
        private readonly ConnectionMapping<string> _connections;

        public AbstractHub(ConnectionMapping<string> connections)
        {
            _connections = connections;
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