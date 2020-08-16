using Microsoft.AspNetCore.Mvc;
using SocketServer.POC.Models;
using SocketServer.POC.Sockets;
using System.Threading.Tasks;
using SocketServer.POC.Extensions;

namespace SocketServer.POC.Controllers
{
    [ApiController]
    [Route("api/push")]
    public class PushController : ControllerBase
    {
        private readonly IPrivateSocketHandler _handler;

        public PushController(IPrivateSocketHandler handler)
        {
            _handler = handler;
        }

        [Route("to")]
        [HttpPost]
        public async Task<IActionResult> PushTo([FromBody] PrivateMessaging message)
        {
            var to = message.To;
            await _handler.SendMessage(to, message.ToJson());
            return Ok();
        }
    }
}