using System;
using System.Text.RegularExpressions;

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

        public DateTime LastVisit { get; set; }

    }

}
