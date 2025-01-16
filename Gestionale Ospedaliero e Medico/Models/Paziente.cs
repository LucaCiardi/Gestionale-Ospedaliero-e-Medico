using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionale_Ospedaliero_e_Medico.Models
{
    public class Paziente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Cognome { get; set; } = string.Empty;

        [Required]
        public DateTime DataNascita { get; set; }

        [Required]
        [StringLength(255)]
        public string Residenza { get; set; } = string.Empty;

        [Required]
        [StringLength(16)]
        public string CodiceFiscale { get; set; } = string.Empty;

        [Required]
        public DateTime DataRicovero { get; set; }

        public int MedicoId { get; set; }

        // Navigation property
        [ForeignKey("MedicoId")]
        public virtual Medico? Medico { get; set; }
    }
}
