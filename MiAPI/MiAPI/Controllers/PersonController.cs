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

        [HttpGet]
        public async Task<List<MPerson>> Get() =>
            await _personCrud.GetAllAsync();
    }
}
