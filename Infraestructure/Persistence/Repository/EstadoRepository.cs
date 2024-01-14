using Domain.Entities;
using Domain.Interfaces.Repository;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repository
{
    public class EstadoRepository
        : IEstadoRepository<Estado, int>
    {
        private readonly DBContext _db;
        public EstadoRepository(DBContext db)
        {
            _db = db;
        }

        public List<Estado> Listar()
        {
            return _db.Estados.ToList();
        }

        public Estado ObtenerPorId(int id)
        {
            var estado = _db.Estados.Find(id) ?? throw new Exception("No se pudo encontrar el estado");
            return estado;
        }
    }
}
