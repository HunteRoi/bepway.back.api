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

        public string Url { get => "http://www.bep-entreprises.be/parcs/"+Nsitid.ToString(); }

        public int Nsitid { get; set; }

        public int NbImplantations { get; set; }

        public string Localisation { get; set; }

        public string Township { get; set; }

        public double Surface { get; set; }
    }
}