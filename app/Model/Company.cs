using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Company
    {
        public decimal Id { get; set; }
        public string IdOpenData { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string SiteUrl { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal? ActivitySectorId { get; set; }
        public string CreatorId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ActivitySector ActivitySector { get; set; }
        public virtual User Creator { get; set; }
    }
}
