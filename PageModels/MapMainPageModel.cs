using System;
using voltaire.PageModels.Base;
using System.Collections.Generic;
using voltaire.Models;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace voltaire.PageModels
{
    public class MapMainPageModel : BasePageModel
    {


        public Command Filter => new Command((obj) =>
       {
           FilterLayoutVisibility = !FilterLayoutVisibility;
       });

        public Command FilterReset => new Command((obj) =>
       {
           Weight = 0;
           GradeFilter = null;

           Customers = AllCustomers;
       });

        public Command FilterWeight => new Command((obj) =>
       {
             Weight = Convert.ToInt32(obj as string);
            FilterOutAddresses();
       });

        public Command PartnerGradeFilter => new Command((obj) =>
       {
            GradeFilter = obj as string;
            FilterOutAddresses();
       });


        Dictionary<string, long?> GradeValues { get; set; }

        int weight=0;
        public int Weight 
        {
            get { return weight; }
            set
            {
                weight = value;
                RaisePropertyChanged();
            }
        }


        bool filtervisible = false;
        public bool FilterLayoutVisibility 
        {
            get { return filtervisible; }
            set
            {
                filtervisible = value;
                RaisePropertyChanged();
            }
        }

        string gradefilter;
        public string GradeFilter
		{
			get { return gradefilter; }
			set
			{
				gradefilter = value;
				RaisePropertyChanged();
			}
		}

        ObservableCollection<PartnerGrade> partnerGrades;
        public ObservableCollection<PartnerGrade> PartnerGrades { get { return partnerGrades; } set { partnerGrades = value;  RaisePropertyChanged(); } }

        List<Partner> AllCustomers;
      
        List<Partner> customers;
        public List<Partner> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                RaisePropertyChanged();
            }
        }

        void FilterOutAddresses()
        {
            List<Partner> filter_list = new List<Partner>();

           

            switch (Weight)
            {
                case 0:
                    {
                        filter_list = AllCustomers.Where((arg) => arg.Weight == 0 ).ToList();
                        break;                                               
                    }
                case 1:
                    {
                        filter_list = AllCustomers.Where((arg) =>  arg.Weight == 1).ToList();
						break;
					}
				case 2:
					{
						filter_list = AllCustomers.Where((arg) =>  arg.Weight == 2).ToList();
						break;
					}
				case 3:
					{
						filter_list = AllCustomers.Where((arg) =>  arg.Weight == 3).ToList();
						break;
					}
				case 4:
					{
						filter_list = AllCustomers.Where((arg) =>  arg.Weight == 4).ToList();
						break;
					}
				case 5:
					{
						filter_list = AllCustomers.Where((arg) => arg.Weight == 5).ToList();
						break;
					}
                default:
                    break;
            }

            Customers = new List<Partner>(filter_list);

        }

        public async override void Init(object initData)
        {
            base.Init(initData);

            //PartnerGrades 
            var _grades = await StoreManager.PartnerGradeStore.GetItemsAsync();

            GradeValues = new Dictionary<string, long?>();

            foreach (var item in _grades)
            {
                GradeValues.Add(item.Name, item.ExternalId);
            }
          
            PartnerGrades = new ObservableCollection<PartnerGrade>(_grades?.Select((arg) => new PartnerGrade() { Grade = arg.Name }));

            //PartnerGrades 

            // Customer
            var Customer_list = await StoreManager.CustomerStore.GetItemsWithValidCordinates();

            //Customers = Customer_list;

            //AllCustomers = Customer_list;

           // Customer
         
        }

    }
}
