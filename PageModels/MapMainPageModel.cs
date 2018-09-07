using System;
using voltaire.PageModels.Base;
using System.Collections.Generic;
using voltaire.Models;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using voltaire.Resources;
using Xamarin.Forms.GoogleMaps;
using System.Threading.Tasks;
using voltaire.Pages;

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

           Customers = VisiblePartners;
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

        int weight = 0;
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
        public ObservableCollection<PartnerGrade> PartnerGrades { get { return partnerGrades; } set { partnerGrades = value; RaisePropertyChanged(); } }

        List<Partner> VisiblePartners;
        List<Partner> AllPartners;

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

            if (VisiblePartners == null)
                return;

            switch (Weight)
            {
                case 0:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 0).ToList();
                        break;
                    }
                case 1:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 1).ToList();
                        break;
                    }
                case 2:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 2).ToList();
                        break;
                    }
                case 3:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 3).ToList();
                        break;
                    }
                case 4:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 4).ToList();
                        break;
                    }
                case 5:
                    {
                        filter_list = VisiblePartners.Where((arg) => arg.Weight == 5).ToList();
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

            Dialog.ShowLoading();

            var Customer_list = await StoreManager.CustomerStore.GetItemsWithValidCordinates();

            if (Customer_list == null)
            {
                Dialog.HideLoading();

                await CoreMethods.DisplayAlert(AppResources.Alert,AppResources.NoCustomerFound,AppResources.Ok);
            }
            else
            {     
                var cust_list = new List<Partner>(Customer_list);

                AllPartners = cust_list;

                Dialog.HideLoading();
            }
            // Customer
        }


        Tuple<double,double,double,double> CalculateBoundingCoordinates(MapSpan region)
        {
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            // Adjust for Internation Date Line (+/- 180 degrees longitude)
            if (left < -180) left = 180 + (180 + left);

            if (right > 180) right = (right - 180) - 180;

            return new Tuple<double, double, double, double>(left,top,right,bottom);
        }

        public void FilterVisibleRegion(MapSpan region)
        {
            if (AllPartners != null && AllPartners.Any())
            {
                //Dialog.ShowLoading(AppResources.Loading);

                var region_bounds = CalculateBoundingCoordinates(region);

                var left = region_bounds.Item1;
                var top = region_bounds.Item2;
                var right = region_bounds.Item3;
                var bottom = region_bounds.Item4;

                var points = AllPartners.Where((arg) => arg.PartnerLongitude <= right && arg.PartnerLongitude >= left && arg.PartnerLatitude >= bottom && arg.PartnerLatitude <= top).Take(250);

                VisiblePartners = points.ToList();

                FilterOutAddresses();

                //Dialog.HideLoading();
            }
        }

    }
}
