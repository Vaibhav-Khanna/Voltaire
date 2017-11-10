using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using voltaire.DataStore.Abstraction;
using voltaire.DataStore.Abstraction.Stores;
using voltaire.DataStore.Implementation;
using voltaire.DataStore.Implementation.Stores;
using voltaire.Pages.Base;
using Xamarin.Forms;

namespace voltaire.PageModels.Base
{
    public class BasePageModel : FreshBasePageModel
    {

        private bool isbusy;
        public bool IsBusy 
        {
            get { return isbusy; }
            set
            {
                isbusy = value;
                RaisePropertyChanged();
            }
        }


        private bool isrefreshing;
        public bool IsRefreshing
        {
            get { return isrefreshing; }
            set
            {
                isrefreshing = value;
                RaisePropertyChanged();
            }
        }

        public string LoadingText { get; set; }

        public bool IsLoading { get; set; }

      
        public static void Init()
        {
         
            DependencyService.Register<IStoreManager, StoreManager>();
            DependencyService.Register<ICustomerStore, CustomerStore>();
            DependencyService.Register<IContractStore, ContractStore>();
            DependencyService.Register<IQuotationStore, QuotationStore>();

        }

        protected IStoreManager StoreManager { get; } = DependencyService.Get<IStoreManager>();


        public BasePageModel()
        {
            PageWasPopped += OnPageWasPopped;
        }

       
        protected virtual void ReleaseResources()
        {
            
        }


        private void OnPageWasPopped(object sender, EventArgs eventArgs)
        {
            if (CurrentPage.GetType() != typeof(BaseDisposePage))
            {
				return;
            }
               
            ((BaseDisposePage)CurrentPage).DisposeResources();
            PageWasPopped -= OnPageWasPopped;
            ReleaseResources();
        }

        private async Task ExecuteBackCommandAsync()
        {
            await CoreMethods.PopPageModel();
        }

    }
}