using System;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;
using voltaire.Models;
using voltaire.PageModels;
using Xamarin.Forms;
using System.Linq;

namespace voltaire.PopUps
{
    public class AddTagsPopUpModel : BaseModel
    {
        public AddTagsPopUpModel()
        {
            if(ContactsPageModel.GradeValues.Any())
            TagSource = new ObservableCollection<string>(ContactsPageModel.GradeValues.Keys.Select((string arg) => arg));

            SelectedItem = null;
        }

        string selecteditem;
        public string SelectedItem 
        { 
            get { return selecteditem; }
            set
            {
                selecteditem = value;

                RaisePropertyChanged();
            }
        }

        public delegate void EventHandler();

        public event EventHandler ItemSelectedChanged; //  event handler when a item is selected

        public Command Done => new Command((obj) =>  // close button command
        {
            ItemSelectedChanged.Invoke();
            PopupNavigation.PopAsync(true);
        });

        public Command Close => new Command((obj) =>  // close button command
        {
            SelectedItem = null;
            ItemSelectedChanged.Invoke();
            PopupNavigation.PopAsync(true);
        });


        ObservableCollection<string> tagsource;  // Product list
        public ObservableCollection<string> TagSource
        {
            get { return tagsource; }
            set
            {
                tagsource = value;
                RaisePropertyChanged();
            }
        }

    }
}
