using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class EstadoService
        : IEstadoService<Estado, int>
    {
        private IEstadoRepository<Estado, int> _estadoRepository;

        public EstadoService(IEstadoRepository<Estado, int> repository)
        {
            _estadoRepository = repository;
        }

        public List<Estado> Listar()
        {
            return _estadoRepository.Listar();
        }

        public Estado ObtenerPorId(int id)
        {
            return _estadoRepository.ObtenerPorId(id);
        }
    }
}
