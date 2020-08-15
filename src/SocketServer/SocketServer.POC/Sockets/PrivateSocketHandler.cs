using SocketServer.POC.Extensions;
using SocketServer.POC.Models;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.POC.Sockets
{
    public interface IPrivateSocketHandler : ISocketHandler
    {
    }

    public class PrivateSocketHandler : AbstractSocketHandler, IPrivateSocketHandler
    {
        public PrivateSocketHandler(IPrivateSocketManager socketManager) : base(socketManager)
        {
        }

        public override async Task OnConnected(string id, WebSocket socket)
        {
            await base.OnConnected(id, socket);
            var payload = (new PrivateMessaging("server", id, "Connection Established")).ToJson();
            await SendMessage(id, payload);
        }

        public override async Task Receive(WebSocket socket, string payload)
        {
            var model = payload.FromJson<PrivateMessaging>();

            payload = (new PrivateMessaging(model.From, model.To, model.Text)).ToJson();
            await SendMessage(model.To, payload);
        }
    }
}