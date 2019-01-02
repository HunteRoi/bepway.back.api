using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Coordinates
    {
        public Coordinates()
        {
            Company = new HashSet<Company>();
            Zoning = new HashSet<Zoning>();
        }

        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<Zoning> Zoning { get; set; }
    }
}