using MiAPI.Data.Interfaz;
using MiAPI.Models;
using MiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly ICrud<MPerson> _personCrud;
        private readonly IAuthotizationServices _authorizationServices;
        public PersonController(ICrud<MPerson> crud, IAuthotizationServices services)
        {
            _personCrud = crud;
            _authorizationServices = services;
        }
        /// <summary>
        /// El siguiente metodo se encarga de obtener a todos los usuarios de la plataforma.
        /// </summary>
        /// <returns>Lista de personas</returns>
        [HttpGet]
        public async Task<List<MPerson>> Get() =>
            await _personCrud.GetAllAsync();

        /// <summary>
        /// El siguiente metodo te permite filtrar persona por su IdPerson.
        /// </summary>
        /// <param name="id">Valor a buscar</param>
        /// <returns>Persona filtrada</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            await _personCrud.GetByIdAsync(id)!=null?Ok(await _personCrud.GetByIdAsync(id)):NotFound();

        /// <summary>
        /// Actualiza la informacion de la persona activa
        /// </summary>
        /// <param name="person"></param>
        /// <returns>Codigo de estado</returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] MPerson person)
        {
            if (ModelState.IsValid)
            {
                if (await _personCrud.UpdateAsync(person, _authorizationServices.GetUserToken(User)))
                {
                    return Ok(await _personCrud.SelectByName(_authorizationServices.GetUserToken(User)));
                }
                return Conflict();
            }
            return BadRequest();
        }
    }
}
