using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SME_API_Apimanagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(new { UserId = id, Name = "John Doe" });
        }
    }
}
