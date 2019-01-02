using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Company
    {
        [Required]
        public decimal Id { get; set; }

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
        public DateTime CreationDate { get; set; }

        [DefaultValue(false)]
        public Boolean IsPremium { get; set; }

        public byte[] RowVersion { get; set; }

        public ActivitySector ActivitySector { get; set; }

        [Required]
        public Coordinates Coordinates { get; set; }

        public User Creator { get; set; }

        public Zoning Zoning { get; set; }
    }
}