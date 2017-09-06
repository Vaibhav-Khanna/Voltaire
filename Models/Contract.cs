using System;
using System.Collections.Generic;
using System.IO;

namespace voltaire.Models
{
    public class Contract
    {       
        public string Name { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        public string Subject { get; set; }

        public DateTime? DateFrom { get; set; } = DateTime.Now;

        public DateTime? DateTo { get; set; } = DateTime.Now.AddDays(1);

        public List<Agreement> Agreements { get; set; }

        public byte[] SignImageSource { get; set; }
    }
}
