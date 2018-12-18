using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Company
    {
        [Required]
        public decimal Id { get; set; }
        [Required]
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
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public byte[] RowVersion { get; set; }
        public ActivitySector ActivitySector { get; set; }
        public User Creator { get; set; }
    }
}