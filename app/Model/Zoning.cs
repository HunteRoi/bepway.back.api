using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Zoning
    {
        public Zoning()
        {
            Company = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string IdOpenData { get; set; }
        public string Name { get; set; }
        public int CoordinatesId { get; set; }
        public int Nsitid { get; set; }
        public string Localisation { get; set; }
        public string Township { get; set; }
        public string Surface { get; set; }

        public virtual Coordinates Coordinates { get; set; }
        public virtual ICollection<Company> Company { get; set; }
    }
}
