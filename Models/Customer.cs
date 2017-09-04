using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace voltaire.Models
{
    
    public class Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int? Weight { get; set; } 

        public string Grade { get; set; }

        public string Website { get; set; }

        public List<string> Tags { get; set; }

        public Nullable<DateTime> LastVisit { get; set; }

        public bool CanEdit { get; set; } = false;

        public List<CustomerAddressLocation> CustomerAddresses { get; set; }

        public List<QuotationsModel> Quotations { get; set; } = new List<QuotationsModel>();

        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }

}
