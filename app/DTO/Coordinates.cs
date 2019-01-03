using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Coordinates
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public double Longitude { get; set; }
    }
}