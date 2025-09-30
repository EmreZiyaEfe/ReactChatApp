using Backend.Business.Abstract;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly HttpClient _httpClient = new HttpClient();


        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        private async Task<string> GetSentimentAI(string messageText)
        {
            var aiUrl = "https://emre1111-chatappv2.hf.space/run/predict";

            var response = await _httpClient.PostAsJsonAsync(aiUrl, new { data = new[] { messageText } });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<dynamic>();
                return result?.data[0]; // POSITIVE / NEGATIVE / NEUTRAL
            }
            return "Unknown";
        }


        //[HttpPost]
        //private async Task<string> GetSentimentAI(string messageText)
        //{
        //    //var aiUrl = "https://emre1111-chatapp-ai-service.hf.space/run/predict";
        //    var aiUrl = "https://huggingface.co/spaces/emre1111/chatapp-ai-service/queue/predict/";


        //    try
        //    {
        //        var payload = new { data = new[] { messageText } };
        //        var response = await _httpClient.PostAsJsonAsync(aiUrl, payload);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // Raw JSON logla, ne döndüğünü görmek için
        //            var raw = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine("AI Response: " + raw);

        //            // Nested array olabileceğini düşünerek al
        //            var result = await response.Content.ReadFromJsonAsync<dynamic>();
        //            await Console.Out.WriteLineAsync("***********"+result+"*****");
        //            return result?.data?[0]?[0]?.ToString() ?? "Unknown"; // artık doğru değer dönecek
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("AI Error: " + ex.Message);
        //    }

        //    return "Unknown";
        //}


        [HttpGet("GetMessages/{userId}")]
        public async Task<IActionResult> GetMessages(int userId)
        {
            var messages = await _messageService.GetMessagesWithUserAsync(userId);
            return Ok(messages);
        }

        [HttpPost("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            message.SentimentLabel = await GetSentimentAI(message.Text);

            var createdMessage = await _messageService.CreateMessageAsync(message); 

            return CreatedAtAction(nameof(GetMessages), new {userId = createdMessage.UserId}, createdMessage);
        }
    }
}
