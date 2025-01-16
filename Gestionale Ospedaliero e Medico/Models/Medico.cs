using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionale_Ospedaliero_e_Medico.Models
{
    public class Medico
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Cognome { get; set; } = string.Empty;

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [StringLength(255)]
        public string Residenza { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Reparto { get; set; } = string.Empty;

        public bool Primario { get; set; }

        public int PazientiGuariti { get; set; }

        public int TotaleDecessi { get; set; }

        public int OspedaleId { get; set; }

        // Navigation properties
        [ForeignKey("OspedaleId")]
        public virtual Ospedale? Ospedale { get; set; }

        public virtual ICollection<Paziente> Pazienti { get; set; } = new List<Paziente>();
    }
}
