using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Infraestructure.Persistence.Context;

namespace Infraestructure.Persistence.Repository
{
    public class AuthRepository
        : IAuthRepository<Usuario, LoginDTO>
    {
        private DBContext _db;
        public AuthRepository(DBContext db)
        {
            _db = db;
        }
        public Usuario Login(LoginDTO login)
        {
            var user = _db.Usuarios
                          .Where(x => x.Correo == login.Correo)
                          .FirstOrDefault() ?? throw new Exception("No se pudo encontrar el usuario");

            return user;
        }
    }
}
