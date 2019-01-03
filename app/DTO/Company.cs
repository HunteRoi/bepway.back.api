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
        
        [Required]
        public string Address { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Url]
        public string SiteUrl { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
        
        public ActivitySector ActivitySector { get; set; }

        [Required]
        public Coordinates Coordinates { get; set; }

        [Required]
        public int ZoningId { get; set; }
        
        public string CreatorId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [DefaultValue(false)]
        public bool IsPremium { get; set; }

        public byte[] RowVersion { get; set; }
    }
}