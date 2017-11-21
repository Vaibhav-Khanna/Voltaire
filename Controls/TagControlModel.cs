using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using Xamarin.Forms;

namespace voltaire.Controls
{
    public class TagControlModel : BaseModel
    {
        
        public TagControlModel(ObservableCollection<TagControlModel> parentList)
        {
            ParentList = parentList;
           
        }

        private ObservableCollection<TagControlModel> ParentList;
 

        string tagtext;
        public string TagText 
        { 
            get { return tagtext; }
            set
            {
                tagtext = value;
                RaisePropertyChanged();
            }
        }

        bool canedit;
        public bool CanEdit
        {
            get { return canedit; }
            set
            {
                canedit = value;
                RaisePropertyChanged();
            }
        }


        public Command RemoveTag => new Command((obj) =>
       {
           
            ParentList.Remove(this);
       });


    }
}
