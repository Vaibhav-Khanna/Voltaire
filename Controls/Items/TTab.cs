using System;
using System.ComponentModel;
using FreshMvvm;
using voltaire.Models;
using voltaire.PageModels;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.Controls.Items
{
    public class TTab : BaseModel
    {
        public string Name { get; set; }

        public Type View { get; set; }

        public Partner Customer { get; private set; }

        public IPageModelCoreMethods Navigation { get; private set; }

        object parent;

        public object Parent 
        {
            get { return parent; }
            set 
            {
                parent = value;

                var cast_parent = (BasePageModel)parent;

                if(cast_parent!=null){

                    Navigation = cast_parent.CoreMethods;

                    var contactdetailpage = (ContactDetailPageModel)cast_parent;

                    if(contactdetailpage!=null)
                    {
                        Customer = contactdetailpage.customer;
                    }

                }

                RaisePropertyChanged();
            }
        } 

        public object ViewBindingContext { get; set; }

        public TTab(BasePageModel ParentViewModel)
        {
            Parent = ParentViewModel;
        }

        public virtual void OnAppearing()
        {
            (ViewBindingContext as BasePageModel)?.TabAppearing();
        }

    }
}
