using System.ComponentModel.DataAnnotations;

namespace Encurtador.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string URL { get; set; }
        
        [Required]
        public string Encurtador { get; set; }
        public int? Visitas { get; set; }
    }
}
