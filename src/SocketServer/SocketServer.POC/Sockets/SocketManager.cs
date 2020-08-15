using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer.POC.Sockets
{
    public interface ISocketManager
    {
        ConcurrentDictionary<string, WebSocket> Connections { get; set; }
        Task AddConnection(string id, WebSocket socket);
        Task<WebSocket> GetSocketById(string id);
        Task<string> GetSocketId(WebSocket socket);
        Task RemoveSocketById(string id);
    }

    public abstract class AbstractSocketManager : ISocketManager
    {
        public ConcurrentDictionary<string, WebSocket> Connections { get; set; } = new ConcurrentDictionary<string, WebSocket>();

        public async Task AddConnection(string id, WebSocket socket)
        {
            Connections.AddOrUpdate(id, socket, (key, old) => socket);
        }

        public async Task<WebSocket> GetSocketById(string id)
        {
            return Connections.FirstOrDefault(p => p.Key.Equals(id)).Value;
        }

        public async Task<string> GetSocketId(WebSocket socket)
        {
            return Connections.FirstOrDefault(p => p.Value.Equals(socket)).Key;
        }

        public async Task RemoveSocketById(string id)
        {
            Connections.TryRemove(id, out var socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection Closed", CancellationToken.None);
        }
    }
}