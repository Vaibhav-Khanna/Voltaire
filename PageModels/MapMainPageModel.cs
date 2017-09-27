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
       });

        public Command FilterWeight => new Command((obj) =>
       {
             Weight = Convert.ToInt32(obj as string);
       });

        public Command PartnerGradeFilter => new Command((obj) =>
       {
            GradeFilter = obj as string;
       });


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


        public ObservableCollection<PartnerGrade> partnerGrades { get; set; }


        List<Customer> AllCustomers;
        List<Customer> customers;
        public List<Customer> Customers
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
            List<Customer> filter_list = new List<Customer>();

            switch (Weight)
            {
                case 0:
                    {
                        filter_list = AllCustomers.Where((arg) => arg.Weight == null).ToList();
                        break;                                               
                    }
                case 1:
                    {
                        filter_list = AllCustomers.Where((arg) => arg.Weight != null && arg.Weight == 1).ToList();
						break;
					}
				case 2:
					{
						filter_list = AllCustomers.Where((arg) => arg.Weight != null && arg.Weight == 2).ToList();
						break;
					}
				case 3:
					{
						filter_list = AllCustomers.Where((arg) => arg.Weight != null && arg.Weight == 3).ToList();
						break;
					}
				case 4:
					{
						filter_list = AllCustomers.Where((arg) => arg.Weight != null && arg.Weight == 4).ToList();
						break;
					}
				case 5:
					{
						filter_list = AllCustomers.Where((arg) => arg.Weight != null && arg.Weight == 5).ToList();
						break;
					}
                default:
                    break;
            }


            Customers = new List<Customer>(filter_list);

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            // Mock Data
            AllCustomers = new List<Customer>();

			partnerGrades = new ObservableCollection<PartnerGrade>
			{
				new PartnerGrade {Grade="CCE"},
				new PartnerGrade {Grade="CSO"},
				new PartnerGrade {Grade="DRE"},
				new PartnerGrade {Grade="Endurance"},
				new PartnerGrade {Grade="PRO"},
				new PartnerGrade {Grade="Particulier"},
				new PartnerGrade {Grade="Ecuries"}
            };
            // mock data

            FilterOutAddresses();
        }

    }
}
