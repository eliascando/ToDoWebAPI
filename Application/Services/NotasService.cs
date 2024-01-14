using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class NotasService
        : IServiceBase<Nota, Guid>,
          INota<NotaDTO>
    {
        private readonly IRepositoryBase<Nota, Guid> _nota;
        private readonly IEstadoRepository<Estado, int> _estado;
        private readonly INota<Nota> _notaUsuario;

        public NotasService(
            IRepositoryBase<Nota, Guid> repository,
            IEstadoRepository<Estado, int> estado,
            INota<Nota> notaUsuario
        )
        {
            _nota = repository;
            _estado = estado;
            _notaUsuario = notaUsuario;
        }

        public Nota Actualizar(Nota entidad)
        {
            var nota = _nota.Actualizar(entidad);
            return nota;
        }

        public Nota ActualizarNotaDTO(EditarNotaDTO dto)
        {
            var nota =_nota.ObtenerPorId(dto.Id);

            nota.Titulo = dto.Titulo;
            nota.Texto = dto.Texto;

            var notaActualizada = _nota.Actualizar(nota);
            return notaActualizada;
        }

        public Nota Agregar(Nota entidad)
        {
            var nota = _nota.Agregar(entidad);
            return nota;
        }

        public Nota AgregarNotaDTO(CrearNotaDTO dto)
        {
            int ID_ESTADO = 1; // Pendiente

            var nota = new Nota
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Texto = dto.Texto,
                FechaCreacion = DateTime.Now,
                IdUsuario = dto.IdUsuario,
                IdEstado = ID_ESTADO
            };  

            var notaAgregada = _nota.Agregar(nota);
            return notaAgregada;
        }

        public bool Eliminar(Guid id)
        {
            var nota = _nota.Eliminar(id);
            return nota;
        }

        public List<Nota> Listar()
        {
            return _nota.Listar();
        }

        public Nota ObtenerPorId(Guid id)
        {
            return _nota.ObtenerPorId(id);
        }
        public List<NotaDTO> ObtenerNotasDeUsuario(Guid id)
        {
            var ListaNotas = _notaUsuario.ObtenerNotasDeUsuario(id);

            var ListaNotasDTO = new List<NotaDTO>();

            foreach(var notes in ListaNotas)
            {
                var newNote = new NotaDTO
                {
                    Id = notes.Id,
                    Titulo = notes.Titulo,
                    Texto = notes.Texto,
                    FechaCreacion = notes.FechaCreacion,
                    Estado = _estado.ObtenerPorId(notes.IdEstado).Nombre
                };

                ListaNotasDTO.Add(newNote);
            }

            return ListaNotasDTO;
        }

        public void ActualizarEstadoNota(Guid id, int estado)
        {
            _notaUsuario.ActualizarEstadoNota(id, estado);
        }
    }
}
