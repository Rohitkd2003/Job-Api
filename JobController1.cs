using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace job_api.Controllers
{
    [Route("[controller]")]
    public class JobController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetJobs()
        {
            var requestUrl = "https://jobs-api14.p.rapidapi.com/list?query=Web%20Developer&location=United%20States&distance=1.0&language=en_GB&remoteOnly=false&datePosted=month&employmentTypes=fulltime%3Bparttime%3Bintern%3Bcontractor&index=0";
            var apiKey = "1813e39c66msh609667f1820a211p100af8jsn5c7284858427"; // Replace with your actual API key

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUrl),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", "jobs-api14.p.rapidapi.com" },
                },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var body = await response.Content.ReadAsStringAsync();
                        var jobData = JsonConvert.DeserializeObject<JobResponse>(body);
                        return View("Jobs", jobData); // Pass data to Razor view
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, new { Message = "API request failed", Details = errorContent });
                    }
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, new { Message = e.Message });
            }
        }
    }

    public class JobResponse
    {
        public Job[] Jobs { get; set; }
    }

    public class Job
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string employmentType { get; set; }
        public string datePosted { get; set; }
        public string salaryRange { get; set; }  // Assuming jobProviders is an array


    }
}
