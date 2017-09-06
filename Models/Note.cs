using System;
namespace voltaire.Models
{
    public class Note
    {
       
        public int id { get; set; }

        public string Publisher { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public bool IsReminderActive { get; set; }

        public Reminder Reminder { get; set; }
    }
}
