using Domain.Entities;
using Domain.Interfaces.Repository;
using Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repository
{
    public class UsuarioRepository : IRepositoryBase<Usuario, Guid>
    {
        private readonly DBContext _db;

        public UsuarioRepository(DBContext db)
        {
            _db = db;
        }
        public Usuario Actualizar(Usuario entidad)
        {
            _db.Usuarios.Attach(entidad);
            _db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();

            var actualizado = _db.Usuarios.Find(entidad.Id) ?? throw new Exception("No se pudo actualizar el usuario");
            return actualizado;
        }

        public Usuario Agregar(Usuario entidad)
        {
            _db.Usuarios.Add(entidad);
            _db.SaveChanges();

            var agregado = _db.Usuarios.Find(entidad.Id) ?? throw new Exception("No se pudo agregar el usuario");
            return agregado;
        }

        public bool Eliminar(Guid id)
        {
            var usuario = _db.Usuarios.Find(id) ?? throw new Exception("No se pudo encontrar el usuario");
            _db.Usuarios.Remove(usuario);
            _db.SaveChanges();

            return true;
        }

        public List<Usuario> Listar()
        {
            return _db.Usuarios.ToList();
        }

        public Usuario ObtenerPorId(Guid id)
        {
            var usuario = _db.Usuarios.Find(id) ?? throw new Exception("No se pudo encontrar el usuario");
            return usuario;
        }
    }
}
