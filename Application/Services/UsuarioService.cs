using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class UsuarioService
        : IServiceBase<Usuario, Guid>
    {
        private readonly IRepositoryBase<Usuario, Guid> _usuario;

        public UsuarioService(IRepositoryBase<Usuario, Guid> repository)
        {
            _usuario = repository;
        }

        public Usuario Actualizar(Usuario entidad)
        {
            var usuario = _usuario.Actualizar(entidad);
            return usuario;
        }

        public Usuario Agregar(Usuario entidad)
        {
            entidad.Id = Guid.NewGuid();
            entidad.Password = BCrypt.Net.BCrypt.HashPassword(entidad.Password);
            var usuario = _usuario.Agregar(entidad);
            return usuario;
        }

        public bool Eliminar(Guid id)
        {
            var usuario = _usuario.Eliminar(id);
            return usuario;
        }

        public List<Usuario> Listar()
        {
            return _usuario.Listar();
        }

        public Usuario ObtenerPorId(Guid id)
        {
            return _usuario.ObtenerPorId(id);
        }
    }
}
