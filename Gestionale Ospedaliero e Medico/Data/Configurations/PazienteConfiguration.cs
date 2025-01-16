using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Data.Configurations
{
    public class PazienteConfiguration : IEntityTypeConfiguration<Paziente>
    {
        public void Configure(EntityTypeBuilder<Paziente> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Cognome)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.CodiceFiscale)
                  .IsRequired()
                  .HasMaxLength(16);

            builder.Property(e => e.Residenza)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.DataNascita)
                  .IsRequired();

            builder.Property(e => e.DataRicovero)
                  .IsRequired()
                  .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(e => e.CodiceFiscale)
                  .IsUnique()
                  .HasName("UQ_Pazienti_CodiceFiscale");
        }
    }
}
