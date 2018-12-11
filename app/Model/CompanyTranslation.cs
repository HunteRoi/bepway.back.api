using System;
using System.Collections.Generic;

namespace Model
{
    public partial class CompanyTranslation
    {
        public decimal Id { get; set; }
        public decimal CompanyId { get; set; }
        public string ActivitySector { get; set; }

        public virtual Company Company { get; set; }
    }
}
