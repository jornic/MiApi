using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ICrud<MPerson> _personCrud;
        public PersonController(ICrud<MPerson> crud)
        {
            _personCrud = crud;
        }
        /// <summary>
        /// El siguiente metodo se encarga de obtener a todos los usuarios de la plataforma.
        /// </summary>
        /// <returns>Lista de personas</returns>
        [HttpGet]
        public async Task<List<MPerson>> Get() =>
            await _personCrud.GetAllAsync();
    }
}
