using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionale_Ospedaliero_e_Medico.Models
{
    public class Ospedale
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Sede { get; set; } = string.Empty;

        public bool Pubblico { get; set; }

        // Navigation property
        public virtual ICollection<Medico> Medici { get; set; } = new List<Medico>();
    }
}
