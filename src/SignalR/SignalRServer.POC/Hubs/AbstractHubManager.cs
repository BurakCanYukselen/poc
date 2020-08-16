using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Extensions;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public abstract class AbstractHubManager<THub, THubMessage, TConnections, TConnectionKey>
        where THub : Hub
        where TConnections : ConnectionMapping<TConnectionKey>
    {
        private readonly IHubContext<THub> _hubContext;
        private readonly TConnections _connections;

        public AbstractHubManager(IHubContext<THub> hubContext, TConnections connections)
        {
            _hubContext = hubContext;
            _connections = connections;
        }

        public async Task Broadcast(AbstractHubMessage<THubMessage, TConnectionKey> message)
        {
            var messageJson = message.ToJson();
            await _hubContext.Clients.All.SendAsync("Receive", messageJson);
        }

        public async Task Send(AbstractHubMessage<THubMessage, TConnectionKey> message)
        {
            var messageJson = message.ToJson();
            var connectionIds = _connections.GetConnections(message.To).ToList();
            await _hubContext.Clients.Clients(connectionIds).SendAsync("Receive", messageJson);
        }
    }
}