using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Connections;
using SignalRServer.POC.Hubs;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Controllers
{
    [ApiController]
    [Route("api/push")]
    public class PushController : ControllerBase
    {
        private readonly MessagingHubManager _messagingHubManager;

        public PushController(MessagingHubManager messagingHubManager)
        {
            _messagingHubManager = messagingHubManager;
        }

        [HttpPost]
        [Route("Broadcast")]
        public async Task<IActionResult> BroadcastMessage([FromBody] MessagingHubModel message)
        {
            _messagingHubManager.Broadcast(new MessagingHubModel()
            {
                From = "Server",
                To = "Everybody",
                Message = message.Message
            });
            return Ok();
        }
        
        [HttpPost]
        [Route("to")]
        public async Task<IActionResult> MessageTo([FromBody] MessagingHubModel message)
        {
            _messagingHubManager.Send(new MessagingHubModel()
            {
                From = "Server",
                To = message.To,
                Message = message.Message
            });
            return Ok();
        }
    }
}