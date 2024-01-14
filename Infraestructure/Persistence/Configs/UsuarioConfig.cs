using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configs
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).IsRequired();
            builder.HasIndex(u => u.Correo).IsUnique();

            builder.HasMany(u => u.Notas)
                   .WithOne(n => n.Usuario)
                   .HasForeignKey(n => n.IdUsuario);
        }
    }
}
