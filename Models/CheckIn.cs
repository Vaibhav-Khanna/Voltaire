using System;

namespace voltaire.Models
{
    public class CheckIn
    {
        public CheckIn(Partner customer)
        {
            Customer = customer;
        }

        public Partner Customer { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now; // Mock data

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }
    }
}
