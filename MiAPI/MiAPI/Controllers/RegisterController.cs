using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IInsert<MfirstRegister> _insert;
        public RegisterController(IInsert<MfirstRegister> insert)
        {
            _insert = insert;
        }

        [HttpPost]
        public async Task<IActionResult> Get(MfirstRegister mfirstRegister)
        {
            if (mfirstRegister == null)
            {
                return BadRequest();
            }else if(await _insert.CreateAsync(mfirstRegister))
            {
                return Ok();
            }else { 
                return BadRequest();             
            }

        }
    }
}
