using Microsoft.AspNetCore.Mvc;
using HelloWebAPI.EmployeeModels;
using Microsoft.EntityFrameworkCore;

namespace HelloWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly EmployeeContext _employeeDbContext;

        public HelloController(IHttpClientFactory httpClientFactory, EmployeeContext employeeContext)
        {
            _httpClientFactory = httpClientFactory;
            _employeeDbContext = employeeContext;
        }

        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok("Hello World!");
        }

        [HttpGet("EmployeeDetails")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDetails()
        {
            return await _employeeDbContext.Employee.ToListAsync();
        }

        //Calling from this API to another remote free API
        [HttpGet("call-remote-api")]
        public async Task<IActionResult> CallRemoteApi()
        {
            var httpClient = _httpClientFactory.CreateClient("RemoteApi");

            // Make an HTTP GET request to the remote API's endpoint.
            HttpResponseMessage response = await httpClient.GetAsync("https://official-joke-api.appspot.com/random_joke"); // Replace with the actual endpoint in the remote API.

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                // Handle the failure case.
                return BadRequest("Failed to call the remote API.");
            }
        }
    }
}
