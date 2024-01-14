using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;

namespace Application.Services
{
    public class AuthenticationService
        : IAuthenticationService<Usuario, LoginDTO>
    {
        private readonly IAuthRepository<Usuario, LoginDTO> _auth;

        public AuthenticationService(IAuthRepository<Usuario, LoginDTO> repository)
        {
            _auth = repository;
        }

        public Usuario Login(LoginDTO login)
        {
            var user = _auth.Login(login);
            
            if(!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                throw new Exception("Contraseña incorrecta");

            return user;
        }
    }
}
