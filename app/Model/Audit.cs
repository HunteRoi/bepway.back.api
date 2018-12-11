using System;
using System.Collections.Generic;

namespace Model
{
    public class Audit
    {
        public decimal Id { get; set; }
        public DateTime EditDate { get; set; }
        public string UserId { get; set; }
        public decimal CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
