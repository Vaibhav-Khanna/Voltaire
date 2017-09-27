using System;

namespace voltaire.Models
{
    public class CourseAgendaCellModel : BaseModel
    {
        
        public CourseAgendaCellModel(CheckIn checkin)
        {
            Customer = checkin.Customer;
            Longitude = checkin.Longitude;
            Latitude = checkin.Latitude;
            CheckInTime = checkin.DateTime.ToString("h:mm tt");
            ContactName = $"{Customer.FirstName} {Customer.LastName}";
            Address = checkin.Address;
            CheckIn = checkin;
        }

        public Customer Customer { get; set; }

        public CheckIn CheckIn { get; set; }

        public string Address { get; set; }

        public double Longitude { get; private set; }

        public double Latitude { get; private set; }

        public string Index { get; set; }

        public string CheckInTime { get; set; } 

        public string ContactName { get; set; }

        public string Duration { get; set; } 

    }
}
