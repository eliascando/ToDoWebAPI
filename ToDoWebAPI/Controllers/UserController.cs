using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ToDoWebAPI.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UsuarioService _service;

        public UserController(UsuarioService service)
        {
            _service = service;
        }

        // Se dejó habilitado solo para registrar usuarios
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Usuario> Post([FromBody] Usuario usuario)
        {
            try
            {
                var user = _service.Agregar(usuario);
                return Ok(JsonConvert.SerializeObject(new { succed = true, message = "Usuario registrado", data = user}));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { succed = false, message = ex.Message, details = ex}));
            }
        }
    }
}
