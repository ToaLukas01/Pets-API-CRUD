using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PetsApiBackend.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Race { get; set; } = string.Empty;
        [Required]
        public string Color { get; set; } = string.Empty;
        [Required]
        public float Weight { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime createDate  { get; set; }
    }
}
