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
using voltaire.Resources;
using Acr.UserDialogs;

namespace voltaire.PageModels.Base
{
    public class BasePageModel : FreshBasePageModel
    {

        protected IUserDialogs Dialog = UserDialogs.Instance;

        public Command BackCommand => new Command( async() =>
       {
            await CoreMethods.PopPageModel();
       });

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

        private bool isloadmore;
        public bool IsLoadMore
        {
            get { return isloadmore; }
            set
            {
                isloadmore = value;
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


        private string isloadingtext = AppResources.Loading;
        public string IsLoadingText
        {
            get { return isloadingtext; }
            set
            {
                isloadingtext = value;
                RaisePropertyChanged();
            }
        }

        private bool isloading;
        public bool IsLoading 
        {
            get { return isloading; }
            set
            {
                isloading = value;
                RaisePropertyChanged();
            }
        }

      
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