using System;
namespace voltaire.Models
{
    public class Salesman
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? CheckIns { get; set; }

        public int? NewContacts { get; set; }

        public State State { get; set; }
    }
}
