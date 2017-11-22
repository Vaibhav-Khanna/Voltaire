using System;
using Xamarin.Forms;

namespace voltaire.Models
{

    public class ContractModel 
    {
        
        public ContractModel(Contract contract)
        {
            Contract = contract;
        }


        Contract contract;
        public Contract Contract 
        {
            get { return contract; }
            set
            {
                contract = value;

                Name = contract.Name;
				Date = contract.ModifiedDateTime.Date;
				Time = contract.ModifiedDateTime.TimeOfDay;
                CustomerName = contract.Customer.Name;
            }
        }

        public string CustomerName { get; set; }

        public Color BackColor { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

    }
}
