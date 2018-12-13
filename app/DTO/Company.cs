using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Company
    {
        [Required]
        public decimal Id { get; set; }
        public string IdOpenData { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string ImageUrl { get; set; }
        [Url]
        public string SiteUrl { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        [Required]
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime? CreationDate { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual DTO.ActivitySector ActivitySector { get; set; }
        public virtual DTO.User Creator { get; set; }
    }
}