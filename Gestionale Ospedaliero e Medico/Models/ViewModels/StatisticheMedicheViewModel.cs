namespace Gestionale_Ospedaliero_e_Medico.Models.ViewModels
{
    public class StatisticheMediciViewModel
    {
        public int MedicoId { get; set; }
        public string NomeMedico { get; set; } = string.Empty;
        public string Reparto { get; set; } = string.Empty;
        public string NomeOspedale { get; set; } = string.Empty;
        public int PazientiAttuali { get; set; }
        public int PazientiGuariti { get; set; }
        public int TotaleDecessi { get; set; }
        public decimal PercentualeSuccesso { get; set; }
    }
}
