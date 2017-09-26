using System;

namespace voltaire.Models
{
    public class CheckIn
    {
        public CheckIn(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now; // Mock data

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
