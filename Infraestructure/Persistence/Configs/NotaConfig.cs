using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configs
{
    public class NotaConfig : IEntityTypeConfiguration<Nota>
    {
        public void Configure(EntityTypeBuilder<Nota> builder)
        {
            builder.ToTable("Notas");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).IsRequired();

            builder.HasOne(n => n.Estado)
                   .WithMany(e => e.Notas)
                   .HasForeignKey(n => n.IdEstado);
        }
    }
}
