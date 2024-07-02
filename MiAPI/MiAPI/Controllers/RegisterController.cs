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

        /// <summary>
        /// El método POST del controlador Register permite registrar a un usuario por primera vez.
        /// </summary>
        /// <param name="mfirstRegister">Se espera un modelo con los atributos: Name, LastName, PhoneNumber, Email, City, Address, User y password</param>
        /// <returns>Codigo de estado HTTP</returns>
        [HttpPost]
        public async Task<IActionResult> Get(MfirstRegister mfirstRegister)
        {
            if (mfirstRegister == null || mfirstRegister.Name == "string")
            {
                return BadRequest();
            }else if(await _insert.CreateAsync(mfirstRegister))
            {
                return Ok();
            }else { 
                return Conflict("Por favor, revisa los datos enviados. No es posible repetir el usuario.");             
            }

        }
    }
}
