namespace Gestionale_Ospedaliero_e_Medico.Models
{
    public class PazienteFilterModel
    {
        public string? SearchTerm { get; set; }
        public DateTime? DataRicoveroInizio { get; set; }
        public DateTime? DataRicoveroFine { get; set; }
        public int? MedicoId { get; set; }
        public string? Reparto { get; set; }
        public string? SortBy { get; set; }
        public bool SortAscending { get; set; } = true;
    }

    public class MedicoFilterModel
    {
        public string? SearchTerm { get; set; }
        public string? Reparto { get; set; }
        public bool? Primario { get; set; }
        public int? OspedaleId { get; set; }
        public string? SortBy { get; set; }
        public bool SortAscending { get; set; } = true;
    }

    public class OspedaleFilterModel
    {
        public string? SearchTerm { get; set; }
        public string? Sede { get; set; }
        public bool? Pubblico { get; set; }
        public string? SortBy { get; set; }
        public bool SortAscending { get; set; } = true;
    }

}
