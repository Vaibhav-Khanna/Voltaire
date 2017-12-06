using System;
namespace voltaire.Models
{
    public class Reminder
    {
        public string Name { get; set; }

        public DateTime ReminderDateTime { get; set; }

        public ReminderPriority Priority { get; set; } = ReminderPriority.None;
    }

    public enum ReminderPriority
    {
        None, Low , Medium , High
    }
}
