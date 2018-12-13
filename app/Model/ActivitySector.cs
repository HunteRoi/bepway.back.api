using System;
using System.Collections.Generic;

namespace Model
{
    public partial class ActivitySector
    {
        public ActivitySector()
        {
            Company = new HashSet<Company>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Company> Company { get; set; }
    }
}
