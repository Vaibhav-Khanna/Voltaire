using System;
namespace voltaire.Models
{
    public class Note
    {

        public string Publisher { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        // public bool IsReminderActive { get; set; }

        // public Reminder Reminder { get; set; } = new Reminder() { Priority = ReminderPriority.None, ReminderDateTime = DateTime.Now };
    }
}
