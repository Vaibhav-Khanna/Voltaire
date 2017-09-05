using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class Agreement
    {
        
        public string Title { get; set; }

        public bool IsSelected { get; set; } = false;

        public List<FormattedString> ContractString { get; set; }

		public Agreement ObjectClone()
		{
			return (Agreement) this.MemberwiseClone();
		}

    }
}
