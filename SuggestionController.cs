using Microsoft.AspNetCore.Mvc;

namespace job_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public SuggestionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("companies")]
        public async Task<IActionResult> GetCompanySuggestions(string query)
        {
            var apiKey = "YOUR_CLEARBIT_API_KEY";
            var requestUrl = $"https://autocomplete.clearbit.com/v1/companies/suggest?query={query}&key={apiKey}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return BadRequest("Failed to fetch company names");
        }
    }
}
