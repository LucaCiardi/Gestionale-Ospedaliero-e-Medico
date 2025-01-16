using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Data.Configurations
{
    public class OspedaleConfiguration : IEntityTypeConfiguration<Ospedale>
    {
        public void Configure(EntityTypeBuilder<Ospedale> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.Nome, e.Sede })
                  .IsUnique()
                  .HasName("UQ_Ospedali_Nome_Sede");

            builder.Property(e => e.Nome)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Sede)
                  .IsRequired()
                  .HasMaxLength(255);

            // Configurazione della relazione uno-a-molti con Medici
            builder.HasMany(o => o.Medici)
                  .WithOne(m => m.Ospedale)
                  .HasForeignKey(m => m.OspedaleId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
