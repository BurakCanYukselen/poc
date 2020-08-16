using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public class MessagingHub: AbstractHub<string>
    {
        public MessagingHub(MessagingConnection connections) : base(connections)
        {
        }
    }
}