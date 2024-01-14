using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ToDoWebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private AuthenticationService _authentication;
        private AuthorizationService _authorization;

        public AuthController(AuthenticationService authentication, AuthorizationService authorization)
        {
            _authentication = authentication;
            _authorization = authorization;
        }

        [HttpPost]
        public ActionResult<AuthResponseDTO> Post([FromBody] LoginDTO login)
        {
            try
            {
                var user = _authentication.Login(login);
                var token = _authorization.GenerateToken(user);
                var response = new AuthResponseDTO
                {
                    Id = user.Id,
                    Correo = user.Correo,
                    Nombres = user.Nombres + " " + user.Apellidos,
                    Token = token
                };
                return Ok(JsonConvert.SerializeObject(new { success = true, message = "Login exitoso", data = response }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { success = false, message = ex.Message, details = ex }));
            }
        }
    }
}
