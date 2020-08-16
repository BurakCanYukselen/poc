using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    using THub = MessagingHub;
    using TMessageModel = String;
    using TConnection = MessagingConnection;
    using TConncetionKey = String;

    public class MessagingHubManager : AbstractHubManager<THub, TMessageModel, TConnection, TConncetionKey>
    {
        public MessagingHubManager(IHubContext<MessagingHub> hubContext, MessagingConnection connections) : base(hubContext, connections)
        {
        }
    }

    public class MessagingHub : AbstractHub<TConncetionKey>
    {
        public MessagingHub(MessagingConnection connections) : base(connections)
        {
        }
    }

    public class MessagingHubModel : AbstractHubMessage<TMessageModel, TConncetionKey>
    {
    }

    public class MessagingConnection : ConnectionMapping<TConncetionKey>
    {
    }
}