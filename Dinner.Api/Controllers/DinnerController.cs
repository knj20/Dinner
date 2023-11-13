using Microsoft.AspNetCore.Mvc;

namespace Dinner.Api.Controllers
{
    
    [Route("Dinner")]
    public class DinnerController : ApiController
    {
        [HttpGet]
        public IActionResult GetDinners()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
