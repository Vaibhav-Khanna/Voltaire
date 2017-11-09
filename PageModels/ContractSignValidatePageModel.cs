using System;
using Xamarin.Forms;
using voltaire.PageModels.Base;
using voltaire.Models;
using System.IO;
using voltaire.DataStore;
using voltaire.DataStore.Implementation;

namespace voltaire.PageModels
{
    public class ContractSignValidatePageModel : BasePageModel
    {
        
		public Command BackButton => new Command(async (obj) =>
		{
            await CoreMethods.PopPageModel(Contract);
		});

		Contract contract;
		public Contract Contract
		{
			get { return contract; }
			set
			{
				contract = value;

				Title = contract.Name;

				RaisePropertyChanged();
			}
		}

        Stream imagestream;
        public Stream ImageStream
        {
            get { return imagestream; }
            set
            {
                imagestream = value;

                contract.SignImageSource = StoreManager.ReadFully(imagestream);

                BackButton.Execute(null);
            }
        }

		string title;
		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				RaisePropertyChanged();
			}
		}

		public override void Init(object initData)
		{
			base.Init(initData);

			var _contract = initData as Contract;

			if (_contract != null)
			{
				Contract = _contract;
			}
		}


    }
}
