using System;
using System.Collections.Generic;

namespace voltaire.Models
{
    public class Contract
    {       
        public string Name { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string Subject { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<Agreement> Agreements = new List<Agreement>();

    }
}
