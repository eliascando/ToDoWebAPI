using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repository
{
    public class NotaRepository 
        : IRepositoryBase<Nota, Guid>, 
          INota<Nota>
    {
        private readonly DBContext _db;
        public NotaRepository(DBContext db) 
        { 
            _db = db;
        }
        public Nota Actualizar(Nota entidad)
        {
            _db.Notas.Attach(entidad);
            _db.Entry(entidad).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();

            var actualizado = _db.Notas.Find(entidad.Id) ?? throw new System.Exception("No se pudo actualizar la nota");
            return actualizado;
        }

        public void ActualizarEstadoNota(Guid id, int estado)
        {
            var nota = _db.Notas.Find(id) ?? throw new System.Exception("No se pudo encontrar la nota");
            nota.IdEstado = estado;
            
            _db.Notas.Attach(nota);
            _db.Entry(nota).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return;
        }

        public Nota Agregar(Nota entidad)
        {
            _db.Notas.Add(entidad);
            _db.SaveChanges();

            var agregado = _db.Notas.Find(entidad.Id) ?? throw new System.Exception("No se pudo agregar la nota");
            return agregado;
        }

        public bool Eliminar(Guid id)
        {
            var nota = _db.Notas.Find(id) ?? throw new System.Exception("No se pudo encontrar la nota");
            _db.Notas.Remove(nota);
            _db.SaveChanges();

            return true;
        }

        public List<Nota> Listar()
        {
            return _db.Notas.ToList();
        }

        public List<Nota> ObtenerNotasDeUsuario(Guid id)
        {
            return _db.Notas.Where(x => x.IdUsuario == id).ToList();
        }

        public Nota ObtenerPorId(Guid id)
        {
            var nota = _db.Notas.Find(id) ?? throw new Exception("No se pudo encontrar la nota");
            return nota;
        }
    }
}
