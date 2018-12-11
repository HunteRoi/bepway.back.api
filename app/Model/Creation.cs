﻿using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Creation
    {
        public decimal Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserId { get; set; }
        public decimal CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
