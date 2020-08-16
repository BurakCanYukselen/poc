using System.Net.WebSockets;
using System.Text;
using SocketServer.POC.Helpers;

namespace SocketServer.POC.Extensions
{
    public static class WebSocketReceiveResultExtension
    {
        public static string GetContent(this WebSocketReceiveResult result, byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        } 
    }
}