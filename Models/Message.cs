using System;

namespace voltaire.Models
{
    public class Message
    {

        public int Id { get; set; }

        public string Sender { get; set; }

        public DateTime SentDate { get; set; }

        public string Text { get; set; }

    }
}
