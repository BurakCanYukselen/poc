using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRServer.POC.Hubs;
using SignalRServer.POC.Models;

namespace SignalRServer.POC.Controllers
{
    [ApiController]
    [Route("api/push")]
    public class PushController : ControllerBase
    {
        private readonly MessagingHub _hubContext;

        public PushController(IHubContext<MessagingHub> hubContext)
        {
            _hubContext = hubContext as MessagingHub;
        }

        [HttpPost]
        [Route("Broadcast")]
        public async Task<IActionResult> BroadcastMessage([FromBody] MessagingHubModel message)
        {
            _hubContext.Broadcast(new MessagingHubModel()
            {
                From = "Server",
                To = "Everybody",
                Message = message.Message
            });
            return Ok();
        }
    }
}