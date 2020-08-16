using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Hubs
{
    public class MessagingHub: AbstractHub<string>
    {
    }
}