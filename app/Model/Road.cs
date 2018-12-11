using System;
using System.Collections.Generic;

namespace Model
{
    public class Road
    {
        public Road()
        {
            Address = new HashSet<Address>();
            RoadGeoreference = new HashSet<RoadGeoreference>();
        }

        public decimal Id { get; set; }
        public bool IsPracticable { get; set; }
        public decimal ZoningId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Zoning Zoning { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<RoadGeoreference> RoadGeoreference { get; set; }
    }
}
