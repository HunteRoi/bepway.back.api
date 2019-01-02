using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Zoning
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string IdOpenData { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CoordinatesId { get; set; }

        public string Url { get; set; }
    }
}