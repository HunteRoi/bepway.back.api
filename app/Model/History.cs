using System;
using System.Collections.Generic;

namespace Model
{
    public partial class History
    {
        public decimal Id { get; set; }
        public decimal AddressId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal CompanyId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }
    }
}
