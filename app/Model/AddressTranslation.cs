using System;
using System.Collections.Generic;

namespace Model
{
    public partial class AddressTranslation
    {
        public decimal Id { get; set; }
        public decimal AddressId { get; set; }
        public string City { get; set; }

        public virtual Address Address { get; set; }
    }
}
