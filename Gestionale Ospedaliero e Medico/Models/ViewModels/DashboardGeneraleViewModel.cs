namespace Gestionale_Ospedaliero_e_Medico.Models.ViewModels
{
    public class DashboardGeneraleViewModel
    {
        public int OspedaleId { get; set; }
        public string NomeOspedale { get; set; } = string.Empty;
        public string Sede { get; set; } = string.Empty;
        public int NumeroMedici { get; set; }
        public int NumeroPazienti { get; set; }
        public int TotalePazientiGuariti { get; set; }
        public int TotaleDecessi { get; set; }
    }
}
