using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Company
    {
        public Company()
        {
            Audit = new HashSet<Audit>();
            CompanyTranslation = new HashSet<CompanyTranslation>();
            Creation = new HashSet<Creation>();
            History = new HashSet<History>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Sector { get; set; }
        public string UrlSite { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Audit> Audit { get; set; }
        public virtual ICollection<CompanyTranslation> CompanyTranslation { get; set; }
        public virtual ICollection<Creation> Creation { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
