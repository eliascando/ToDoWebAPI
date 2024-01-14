using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ToDoWebAPI.Controllers
{
    [Route("api/estado")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _service;

        public EstadoController(EstadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Estado>> Get()
        {
            return _service.Listar();
        }

        [HttpGet("{id}")]
        public ActionResult<Estado> Get(int id)
        {
            return _service.ObtenerPorId(id);
        }
    }
}
