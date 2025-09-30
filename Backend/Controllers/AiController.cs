using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("sentiment")]
        public async Task<IActionResult> GetSentiment([FromQuery] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty");
            }

            // Python AI service endpoint (Hugging Face Spaces URL)
            var aiUrl = $"https://huggingface.co/spaces/emre1111/chatapp-ai-service.hf.space/run/predict?data=[\"{message}\"]";
            //https://huggingface.co/spaces/emre1111/chatapp-ai-service

            var response = await _httpClient.GetAsync(aiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Ai service error");
            }

            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}
