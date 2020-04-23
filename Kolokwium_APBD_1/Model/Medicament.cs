using System.ComponentModel.DataAnnotations;

namespace Kolokwium_APBD_1.Properties.Model
{
    public class Medicament
    {
        [Required]
        public int IdMedicament { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }
}