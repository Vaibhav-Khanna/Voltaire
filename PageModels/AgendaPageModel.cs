using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.Resources;
using System.Linq;
using voltaire.Helpers.Extensions;

namespace voltaire.PageModels
{
    public class AgendaPageModel : BasePageModel
    {
       
        public AgendaPageModel()
        {
        }

        ObservableCollection<CourseAgendaCellModel> AllCheckInItems;

        ObservableCollection<CourseAgendaCellModel> courseitems;
        public ObservableCollection<CourseAgendaCellModel> CourseItems 
        {
            get { return courseitems; }
            set
            {
                courseitems = value;

                RaisePropertyChanged();
            }
        }


        string selectedfilter = AppResources.Today;
        public string SelectedFilter
        {
            get { return selectedfilter; }
            set
            {
                selectedfilter = value;
                RaisePropertyChanged();

                FilterCheckinForDateRange();
            }
        }


        DateTime startdate = DateTime.Now;
        public DateTime StartDate 
        {
            get { return startdate; } 
            set
            {
                startdate = value;
                RaisePropertyChanged();

                FilterCheckinForDateRange();
            }
        }

        DateTime enddate = DateTime.Now;
		public DateTime EndDate
		{
			get { return enddate; }
			set
			{
				enddate = value;
				RaisePropertyChanged();

                FilterCheckinForDateRange();
			}
		}

        void FilterCheckinForDateRange()
        {
            if (AllCheckInItems == null || AllCheckInItems.Count == 0)
                return;


            List<CourseAgendaCellModel> new_filter_list;

            if (SelectedFilter==AppResources.None)
            {
                new_filter_list = AllCheckInItems.Where((arg) => arg.CheckIn.DateTime.Date.CompareTo(StartDate.Date) >= 0 && arg.CheckIn.DateTime.Date.CompareTo(EndDate.Date) <= 0).ToList();
                CourseItems = new ObservableCollection<CourseAgendaCellModel>(new_filter_list);
            }
            else if(SelectedFilter == AppResources.Today)
            {
                new_filter_list = AllCheckInItems.Where((arg) => arg.CheckIn.DateTime.Date.CompareTo(DateTime.Today.Date) == 0 ).ToList();
                CourseItems = new ObservableCollection<CourseAgendaCellModel>(new_filter_list);
            }
            else if(SelectedFilter == AppResources.ThisWeek)
            {
                new_filter_list = AllCheckInItems.Where((arg) => arg.CheckIn.DateTime.Date.CompareTo(DateTime.Now.StartOfWeek(DayOfWeek.Monday).Date) >= 0 && arg.CheckIn.DateTime.Date.CompareTo(DateTime.Now.Date) <= 0).ToList();
				CourseItems = new ObservableCollection<CourseAgendaCellModel>(new_filter_list);
            }
            else if(SelectedFilter == AppResources.ThisMonth)
            {
                new_filter_list = AllCheckInItems.Where((arg) => arg.CheckIn.DateTime.Date.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) >= 0 && arg.CheckIn.DateTime.Date.CompareTo(DateTime.Now.Date) <= 0).ToList();
				CourseItems = new ObservableCollection<CourseAgendaCellModel>(new_filter_list);
            }

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            // mock data
            var items = new List<CourseAgendaCellModel>() { };
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer() { FirstName = "Smauel" }){ Address ="palo alot", Latitude = 37.795296, Longitude = -122.443807, DateTime = DateTime.Now }){ Index = "1" });
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer(){ FirstName = "dsdsdd" }){ Address = "Nevada", Latitude = 37.766804, Longitude = -122.432821, DateTime = DateTime.Now.AddDays(-1) }){ Index = "2" });
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer(){ FirstName = "Osdsd" }){ Address = "Mads", Latitude = 37.766262, Longitude = -122.389562,DateTime = DateTime.Now.AddDays(-2) }){ Index = "3" });
            // mock data


            AllCheckInItems = new ObservableCollection<CourseAgendaCellModel>(items);
            CourseItems = AllCheckInItems;

            FilterCheckinForDateRange();
        }

    }
}
