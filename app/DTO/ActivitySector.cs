using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ActivitySector
    {
        [Required]
        public decimal Id { get; set; }

        public string Name { get; set; }
    }
}