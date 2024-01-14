using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace ToDoWebAPI.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
        // Add services to the container.

            // Add controllers to the container.
            services.AddControllers();

            // Add Swagger to the container.
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add DbContext to the container.
            services.AddDbContext<DBContext>(
                options => options.UseSqlServer(configuration["Database:ConnectionString"])
            );

            // Add Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            }); 

            // Add custom services to the container.
            // Instance services
            services.AddScoped<AuthorizationService>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<EstadoService>();
            services.AddScoped<NotasService>();
            services.AddScoped<UsuarioService>();
            
            // Implement services
            services.AddScoped<IAuthenticationService<Usuario, LoginDTO>, AuthenticationService>();
            services.AddScoped<IAuthorizationService<Usuario>, AuthorizationService>();
            services.AddScoped<IEstadoService<Estado, int>, EstadoService>();
            services.AddScoped<IServiceBase<Nota, Guid>, NotasService>();
            services.AddScoped<INota<NotaDTO>, NotasService>();
            services.AddScoped<IServiceBase<Usuario, Guid>, UsuarioService>();

            // Instance of the repositories
            services.AddScoped<AuthRepository>();
            services.AddScoped<EstadoRepository>();
            services.AddScoped<NotaRepository>();
            services.AddScoped<UsuarioRepository>();

            // Implement repositories
            services.AddScoped<IAuthRepository<Usuario, LoginDTO>, AuthRepository>();
            services.AddScoped<IRepositoryBase<Nota, Guid>, NotaRepository>();
            services.AddScoped<IEstadoRepository<Estado, int>, EstadoRepository>();
            services.AddScoped<INota<Nota>, NotaRepository>();
            services.AddScoped<IRepositoryBase<Usuario, Guid>, UsuarioRepository>();

            return services;
        }
    }
}
