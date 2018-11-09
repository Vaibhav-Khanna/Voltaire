using System;
using Xamarin.Forms;

namespace voltaire.Models
{

    public class ContractModel 
    {
        
        public ContractModel(Contract contract)
        {
            Contract = contract;

            Name = contract.OrderNumber;
            Date = contract.CreatedAt;
            Time = contract.CreatedAt.TimeOfDay;
        }


        public Contract Contract { get; set; }


        public string CustomerName { get; set; }

        public Color BackColor { get; set; } = Color.White;

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

    }
}
