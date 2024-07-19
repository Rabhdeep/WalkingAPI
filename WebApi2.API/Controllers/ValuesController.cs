using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(new String[] { "Mamta", "Karan", "Rajbeer" });
        }
    }
}
