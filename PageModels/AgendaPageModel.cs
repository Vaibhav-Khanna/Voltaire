using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using voltaire.Resources;

namespace voltaire.PageModels
{
    public class AgendaPageModel : BasePageModel
    {
       
        public AgendaPageModel()
        {
        }


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
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            var items = new List<CourseAgendaCellModel>() { };
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer())));
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer())));
            items.Add(new CourseAgendaCellModel(new CheckIn(new Customer())));

            CourseItems = new ObservableCollection<CourseAgendaCellModel>(items);

        }

    }
}
