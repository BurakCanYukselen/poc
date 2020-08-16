using System;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SocketServer.POC.Extensions;
using SocketServer.POC.Helpers;
using SocketServer.POC.Sockets;

namespace SocketServer.POC.Middlewares
{
    public class SocketMiddleware
    {
        private readonly RequestDelegate _next;
        private ISocketHandler _socketHandler;

        public SocketMiddleware(RequestDelegate next, ISocketHandler socketHandler)
        {
            _next = next;
            _socketHandler = socketHandler;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;
            
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var id = context.GetFromQuery("id");
            await _socketHandler.OnConnected(id, socket);
            await Receive(socket);
        }

        private async Task Receive(WebSocket socket)
        {
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(BufferSize._4096.GetArraySegment(out var buffer), CancellationToken.None);
                var payload = result.GetContent(buffer);
                
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _socketHandler.Receive(socket, payload);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _socketHandler.OnDisconnected(socket);
                }
            }
        }
    }
}