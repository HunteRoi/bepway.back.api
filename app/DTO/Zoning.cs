using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Zoning
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Coordinates Coordinates { get; set; }

        [Url]
        public string Url { get => "http://www.bep-entreprises.be/parcs/"+Nsitid.ToString(); }

        [Required]    
        public int Nsitid { get; set; }

        [DefaultValue(0)]
        public int NbImplantations { get; set; }

        [Required]
        public string Localisation { get; set; }

        [Required]
        public string Township { get; set; }

        [Required]
        public double Surface { get; set; }
    }
}