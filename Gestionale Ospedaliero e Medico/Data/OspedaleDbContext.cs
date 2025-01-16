using Microsoft.EntityFrameworkCore;
using Gestionale_Ospedaliero_e_Medico.Models;
using Gestionale_Ospedaliero_e_Medico.Models.ViewModels;
using Gestionale_Ospedaliero_e_Medico.Data.Configurations;

namespace Gestionale_Ospedaliero_e_Medico.Data
{
    public class OspedaleDbContext : DbContext
    {
        public OspedaleDbContext(DbContextOptions<OspedaleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ospedale> Ospedali { get; set; }
        public DbSet<Medico> Medici { get; set; }
        public DbSet<Paziente> Pazienti { get; set; }

        // DbSet per le viste
        public DbSet<DashboardGeneraleViewModel> DashboardGenerale { get; set; }
        public DbSet<StatisticheMediciViewModel> StatisticheMedici { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OspedaleConfiguration());
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new PazienteConfiguration());

            // Configurazione viste
            modelBuilder.Entity<DashboardGeneraleViewModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_DashboardGenerale");
            });

            modelBuilder.Entity<StatisticheMediciViewModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_StatisticheMedici");
            });
        }
    }
}
