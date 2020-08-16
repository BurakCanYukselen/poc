using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Extensions;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public abstract class AbstractHub<TConnectionKey> : Hub
    {
        private readonly ConnectionMapping<TConnectionKey> _connections;

        public AbstractHub(ConnectionMapping<TConnectionKey> connections)
        {
            _connections = connections;
        }

        public override Task OnConnectedAsync()
        {
            var userid = Context.GetContextHeaderValue("userid");
            var connectionKey = userid.ConvertTo<TConnectionKey>();
            _connections.Add(connectionKey, Context.ConnectionId);
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userid = Context.GetContextHeaderValue("userid");
            var connectionKey = userid.ConvertTo<TConnectionKey>();
            _connections.Remove(connectionKey, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}