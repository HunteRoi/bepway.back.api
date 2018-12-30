using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Zoning
    {
        public int Id { get; set; }
        public string IdOpenData { get; set; }
        public string Name { get; set; }
        public int CoordinatesId { get; set; }
        public string Url { get; set; }

        public virtual Coordinates Coordinates { get; set; }
    }
}
