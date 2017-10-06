using System;

namespace voltaire.Models
{
    public class MessageModel : BaseModel
    {
        
        public MessageModel(Message message)
        {
            Message = message;
            Name = message.Sender;
            Date = message.SentDate.ToString("d");
            Text = message.Text;
        }

        public Message Message { get; set; }

        int index;
        public int Index 
        { 
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged();
            }
        }

		string name;
		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				RaisePropertyChanged();
			}
		}

		string date;
		public string Date
		{
			get { return date; }
			set
			{
				date = value;
				RaisePropertyChanged();
			}
		}

		string text;
		public string Text
		{
			get { return text; }
			set
			{
				text = value;
				RaisePropertyChanged();
			}
		}

        bool expanded = false;
        public bool Expanded 
        {
            get { return expanded; }
            set
            {
                expanded = value;

                RaisePropertyChanged();
            }
        }

    }
}
