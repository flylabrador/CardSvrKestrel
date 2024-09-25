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
            // 1. �����óB�z�Ȥ�ݪ��ƾ�
            var processedMessage = $"Received message: {message.Content.ToUpper()}";

            int newAge = message.Age + 10;

            // 2. �N�B�z�L���ƾڦ^�����Ȥ��
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

        public int Age { get; set; } // �~��
    }

}
