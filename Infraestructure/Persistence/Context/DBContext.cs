using Domain.Entities;
using Infraestructure.Persistence.Configs;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Persistence.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Estado> Estados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            modelBuilder.ApplyConfiguration(new NotaConfig());
            modelBuilder.ApplyConfiguration(new EstadoConfig());
        }
    }
}
