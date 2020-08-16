using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRClient.POC.Entensions;
using SignalRClient.POC.Models;

namespace SignalRClient.POC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Connect As:");
            var userid = Console.ReadLine();

            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:2000/hub/messaging", options => { options.Headers.Add("userid", userid); })
                .WithAutomaticReconnect()
                .Build();
            await connection.StartAsync();

            connection.On("Receive", (string payload) =>
            {
                var message = payload.FromJson<MessagingHubModel>();
                Console.WriteLine($"{message.From}: {message.Message}");
            });

            Console.ReadKey();
        }
    }
}