using System;
using Xamarin.Forms;
using voltaire.PageModels.Base;
using voltaire.Models;
using System.IO;
using voltaire.DataStore;
using voltaire.DataStore.Implementation;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using voltaire.Models.DataObjects;
using voltaire.Helpers;
using voltaire.Resources;
using Syncfusion.Pdf;

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

                Title = contract.OrderNumber;

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

                Contract.SignImageSource = voltaire.DataStore.Implementation.StoreManager.ReadFully(imagestream);

                ValidateSignatureAndUpload();
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

        async void ValidateSignatureAndUpload()
        {
            Dialog.ShowLoading("");

            if (Contract.SignImageSource != null)
            {
                PdfDocument duplicate = (PdfDocument)Contract.Document.Clone();


                var addPage = duplicate.Pages.Add();

                var width = addPage.GetClientSize().Width;

                PdfGraphics graphics = addPage.Graphics;

                PdfBitmap image = new PdfBitmap(new MemoryStream(contract.SignImageSource));

                graphics.DrawImage(image, new RectangleF(20, 40, width / 2, 60));


                MemoryStream m = new MemoryStream();

                duplicate.Save(m);

                var document = await StoreManager.DocumentStore.GetItemByContractId(Contract.Id, "saleContract");

                if( document == null)
                {
                    var documentItem = new Document() { Path = Contract.Id + '/' + "saleContract.pdf", Name = contract.Id + ".pdf", InternalName = "saleContract", ReferenceKind = "contract", ReferenceId = contract.Id, MimeType = "application/pdf" };

                    var uploaded = await StoreManager.DocumentStore.InsertImage(m.ToArray(), documentItem);
                }
                else
                {
                    await PclStorage.SaveFileLocal(m.ToArray(), document.Id);

                    document.ToUpload = true;

                    await StoreManager.DocumentStore.UpdateAsync(document);
                   
                    await StoreManager.DocumentStore.OfflineUploadSync();
                }

                Contract.ToSend = true;

                var isSent = await StoreManager.ContractStore.UpdateAsync(Contract);

                if(isSent)
                {
                    await CoreMethods.DisplayAlert(AppResources.Alert, AppResources.EmailSent, AppResources.Ok);
                    BackButton.Execute(null);
                }

            }

            Dialog.HideLoading();
             
        }

    }
}
