using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ActivitySector
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}