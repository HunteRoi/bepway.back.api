using System;
using System.Collections.Generic;

namespace Model
{
    public class Zoning
    {
        public Zoning()
        {
            Road = new HashSet<Road>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Road> Road { get; set; }
    }
}
