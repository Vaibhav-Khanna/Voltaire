using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace voltaire.Models
{
    public class Agreement
    {
        public string Title { get; set; }

        public bool IsSelected { get; set; }

        public List<FormattedString> ContractString { get; set; }
    }
}
