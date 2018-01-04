using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class Agreement
    {
        
        public string Title { get; set; }

        public bool IsSelected { get; set; } = false;

        public AgreementText ContractString { get; set; } = new AgreementText();

		public Agreement ObjectClone()
		{
			return (Agreement) this.MemberwiseClone();
		}

    }

}
