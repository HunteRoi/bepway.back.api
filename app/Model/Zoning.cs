using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Zoning
    {
        public Zoning()
        {
            Road = new HashSet<Road>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Road> Road { get; set; }
    }
}
