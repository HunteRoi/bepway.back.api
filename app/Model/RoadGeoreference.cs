using System;
using System.Collections.Generic;

namespace Model
{
    public partial class RoadGeoreference
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal RoadId { get; set; }

        public virtual Road Road { get; set; }
    }
}
