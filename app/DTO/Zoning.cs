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
        public Coordinates Coordinates { get; set; }

        public string Url { get => "http://www.bep-entreprises.be/parcs/"+Ntisid.ToString(); }

        public int Ntisid { get; set; }
    }
}