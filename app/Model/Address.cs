using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Address
    {
        public Address()
        {
            AddressTranslation = new HashSet<AddressTranslation>();
            History = new HashSet<History>();
        }

        public decimal Id { get; set; }
        public decimal Number { get; set; }
        public string PostalBox { get; set; }
        public decimal ZipCode { get; set; }
        public string City { get; set; }
        public string Address1 { get; set; }
        public decimal? FloorNumber { get; set; }
        public decimal RoadId { get; set; }

        public virtual Road Road { get; set; }
        public virtual ICollection<AddressTranslation> AddressTranslation { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
