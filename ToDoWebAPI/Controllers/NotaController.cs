using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ToDoWebAPI.Controllers
{
    [Route("api/nota")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private NotasService _service;
        public NotaController(NotasService service)
        {
            _service = service;
        }

        // Crear una nota
        [HttpPost]
        public ActionResult<Nota> Post([FromBody] CrearNotaDTO dto)
        {
           return _service.AgregarNotaDTO(dto);
        }

        // Actualizar una nota
        [HttpPut]
        public ActionResult<Nota> Put([FromBody] EditarNotaDTO nota)
        {
            return _service.ActualizarNotaDTO(nota);
        }

        // Actualizar el estado de una nota
        [HttpPut("estado/{idNota}/{idEstado}")]
        public ActionResult PutEstado(Guid idNota, int idEstado)
        {
            try
            {
                _service.ActualizarEstadoNota(idNota, idEstado);
                return Ok(JsonConvert.SerializeObject(new { success = true ,message = "Estado actualizado" }));
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(new { message = ex.Message }));
            }
        }

        // Obtener las notas de un usuario
        [HttpGet("usuario/{id}")]
        public ActionResult<List<NotaDTO>> GetNotasDeUsuario(Guid id)
        {
            return _service.ObtenerNotasDeUsuario(id);
        }
    }
}
