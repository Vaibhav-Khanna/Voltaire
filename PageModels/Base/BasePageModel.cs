using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FreshMvvm;
using voltaire.Pages.Base;

namespace voltaire.PageModels.Base
{
    public class BasePageModel : FreshBasePageModel
    {
        public bool IsBusy { get; set; }
        public string LoadingText { get; set; }

        public bool IsLoading { get; set; }

        //  public ICommand BackCommand => SingleExecutionCommand.FromFunc(ExecuteBackCommandAsync);

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

        /* public async Task HandleDataLoadException(Action action)
         {
             try
             {
                 await Task.Factory.StartNew(action);
             }
             catch (DataLoadException ex)
             {
                 await CoreMethods.DisplayAlert("Erreur", $"Unable to load data: {ex.Reason}", "OK");
             }

         } */
    }
}