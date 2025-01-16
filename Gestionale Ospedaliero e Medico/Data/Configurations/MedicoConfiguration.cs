using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Data.Configurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Cognome)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Reparto)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Residenza)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Dob)
                  .IsRequired();

            builder.Property(e => e.Primario)
                  .HasDefaultValue(false);

            builder.Property(e => e.PazientiGuariti)
                  .HasDefaultValue(0);

            builder.Property(e => e.TotaleDecessi)
                  .HasDefaultValue(0);

            // Configurazione della relazione uno-a-molti con Pazienti
            builder.HasMany(m => m.Pazienti)
                  .WithOne(p => p.Medico)
                  .HasForeignKey(p => p.MedicoId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
