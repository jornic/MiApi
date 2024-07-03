using MiAPI.Models;
using MiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthotizationServices _services;
        public AuthController(IAuthotizationServices services)
        {
            _services = services;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MLogin login)
        {
            AuthorizationResponse response = await _services.GetAuthorizationIsValid(login);
            if (response!=null)
            {
                return Ok(response);
            }
            return Unauthorized();
        }
    }
}
