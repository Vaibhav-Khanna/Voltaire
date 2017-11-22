using System;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Collections.Generic;
using voltaire.PopUps;
using Rg.Plugins.Popup.Services;

namespace voltaire.PageModels
{

    public class QuotationsMainPageModel : BasePageModel
    {
       
        CustomerPickerPopupModel popup_context = new CustomerPickerPopupModel(); //  Popup picker model 

      
        public Command AddQuotation => new Command(async (obj) =>
        {
            popup_context.ItemSelectedChanged += Popup_Context_ItemSelectedChanged;    // Subscribe to the event

            await PopupNavigation.PushAsync(new CustomerPickerPopUp() { BindingContext = popup_context }, true);
        });


        async void Popup_Context_ItemSelectedChanged()
        {
            if (popup_context.SelectedItem != null)
            {
                await CoreMethods.PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner, bool, QuotationsModel>(popup_context.SelectedItem, true, null));
            }
            // Unsubscribe from the event
            popup_context.ItemSelectedChanged -= Popup_Context_ItemSelectedChanged;
        }



        public Command TapQuotation => new Command(async(obj) =>
       {
            // mock data
            await CoreMethods.PushPageModel<QuotationDetailViewPageModel>(new Tuple<Partner, bool, QuotationsModel>(new Partner(){  }, false, (obj as QuotationsModel) ));
       });


        ObservableCollection<QuotationsModel> quotationsitemsource;
        public ObservableCollection<QuotationsModel> QuotationsItemSource
        {
            get { return quotationsitemsource; }
            set
            {
                quotationsitemsource = value;
                RaisePropertyChanged();
            }
        }


        public override void Init(object initData)
        {
            base.Init(initData);

            // mock data
            var list = new List<QuotationsModel>();
            list.Add(new QuotationsModel(){ Ref = "1212122"  });
            list.Add(new QuotationsModel() { Ref = "322323" });
            // mock data

            QuotationsItemSource = new ObservableCollection<QuotationsModel>(list);
        }

    }
}
