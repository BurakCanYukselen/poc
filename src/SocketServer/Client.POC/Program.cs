using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SocketServer.POC.Models;

namespace Client.POC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("connect as:");
            var id = Console.ReadLine();

            var uri = new UriBuilder("ws://localhost:5000/ws/order");
            var query = HttpUtility.ParseQueryString(uri.Query);
            query.Add("id", id);
            uri.Query = query.ToString();

            var client = new ClientWebSocket();
            await client.ConnectAsync(uri.Uri, CancellationToken.None);
            var send = Task.Run(async () =>
            {
                Console.WriteLine("message to:");
                var to = Console.ReadLine();
                string message;
                while ((message = Console.ReadLine()) != null && message != string.Empty)
                {
                    var payload = JsonConvert.SerializeObject(new PrivateMessaging(id, to, message));
                    var bytes = Encoding.UTF8.GetBytes(payload);
                    client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }

                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });
            var receive = Receive(client);
            await Task.WhenAll(send, receive);
        }

        public static async Task Receive(ClientWebSocket client)
        {
            var buffer = new byte[4 * 1024];
            while (true)
            {
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var payload = Encoding.UTF8.GetString(buffer, 0, result.Count);
                var message = JsonConvert.DeserializeObject<PrivateMessaging>(payload);
                Console.WriteLine($"{message.From}: {message.Text}");
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }
            }
        }
    }
}