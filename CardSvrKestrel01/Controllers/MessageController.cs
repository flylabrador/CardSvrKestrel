using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CardSvrKestrel01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {


        [HttpOptions]
        public IActionResult Options()
        {
            return Ok();
        }

        // POST: api/message
        [HttpPost]
        public IActionResult ProcessMessage([FromBody] ClientMessage message)
        {
            // 1. 接收並處理客戶端的數據
            var processedMessage = $"Received message: {message.Content.ToUpper()}";

            int newAge = message.Age + 10;

            // 2. 將處理過的數據回應給客戶端
            return Ok(new 
            {
                response = processedMessage,
                age = newAge
            });
        }
    }

    public class ClientMessage
    {
        public string Content { get; set; } = string.Empty;

        public int Age { get; set; } // 年齡
    }

}
