using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketServer.POC.Extensions;

namespace SocketServer.POC.Sockets
{
    public interface ISocketHandler
    {
        Task OnConnected(string id, WebSocket socket);
        Task OnDisconnected(WebSocket socket);
        Task SendMessage(WebSocket socket, string text);
        Task SendMessage(string id, string text);
        Task Receive(WebSocket socket, string payload);
    }

    public abstract class AbstractSocketHandler : ISocketHandler
    {
        public readonly ISocketManager _socketManager;

        public AbstractSocketHandler(ISocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        public async virtual Task OnConnected(string id, WebSocket socket)
        {
            await _socketManager.AddConnection(id, socket);
        }

        public async virtual Task OnDisconnected(WebSocket socket)
        {
            var id = await _socketManager.GetSocketId(socket);
            await _socketManager.RemoveSocketById(id);
        }

        public async Task SendMessage(WebSocket socket, string text)
        {
            if (socket.State != WebSocketState.Open)
                return;

            var arraySegment = text.ToByteArraySegment();
            await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task SendMessage(string id, string text)
        {
            var socket = await _socketManager.GetSocketById(id);
            await SendMessage(socket, text);
        }

        public abstract Task Receive(WebSocket socket, string payload);
    }
}