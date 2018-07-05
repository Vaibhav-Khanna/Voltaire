using System;
using System.Collections.Generic;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Helpers;
using voltaire.Models.DataObjects;
using voltaire.DataStore.Implementation;

namespace voltaire.PageModels
{
    public class QuotationSignPageModel : BasePageModel
    {

        Partner Customer { get; set; }

        public Command BackButton => new Command((obj) =>
       {
             CoreMethods.PopPageModel();
       });

        public Command OpenTermsCommand => new Command(async() =>
        {
            var files =  await StoreManager.GetLegalFiles();

            if(files.ContainsKey(StorageKeys.TermsConditions))
            {
                await CoreMethods.PushPageModel<PdfViewerPageModel>(new Tuple<string,byte[]>("Terms & Conditions",files[StorageKeys.TermsConditions]));
            }
        });

        public Command SignValidate => new Command( async (obj) =>
       {
           if (!TermsConditionSelected)
           {
               await CoreMethods.DisplayAlert("Alert", "Please accept the terms and conditions first in order to validate the quotation.", "Ok");
               return;
           }

           if (IsSigned)
           {
               await CoreMethods.DisplayAlert("Alert", "You have already signed this quotation !", "Ok");
               return;
           }

           if (SignImage != null)
           {
               Dialog.ShowLoading("");

               quotation.SignedImage = SignImage;

               IsSigned = true;

                var vendorItem = await StoreManager.DocumentStore.GetItemByEmployeeId(quotation.SaleOrder.Id, "vendor");

               var customeritem = await StoreManager.DocumentStore.GetItemByEmployeeId(quotation.SaleOrder.Id, "customer");

               var invoice = new InvoiceGenerate();

                var customerPDF = invoice.CreatePdfFile(Quotation, Orderitems, Customer, null, false);

                var vendorPDF = invoice.CreatePdfFile(Quotation, Orderitems, Customer, null, true);

               if (vendorItem == null) // nothing exists in storage
               {
                    var document = new Document() { Path = quotation.SaleOrder.Id + '/' + "customer.pdf", Name = quotation.SaleOrder.Id + ".pdf", InternalName = "customer", ReferenceKind = "saleOrder", ReferenceId = quotation.SaleOrder.Id, MimeType = "application/pdf" };

                   await StoreManager.DocumentStore.InsertImage(customerPDF, document);

                   var documentVendor = new Document() { Path = quotation.SaleOrder.Id + '/' + "vendor.pdf", Name = quotation.SaleOrder.Id + ".pdf", InternalName = "vendor", ReferenceKind = "saleOrder", ReferenceId = quotation.SaleOrder.Id, MimeType = "application/pdf" };

                   await StoreManager.DocumentStore.InsertImage(vendorPDF, documentVendor);
               }
               else
               {
                   await PclStorage.SaveFileLocal(vendorPDF, vendorItem.Id);

                   await PclStorage.SaveFileLocal(customerPDF, customeritem.Id);

                   vendorItem.ToUpload = true;
                   customeritem.ToUpload = true;

                   await StoreManager.DocumentStore.UpdateAsync(customeritem);
                   await StoreManager.DocumentStore.UpdateAsync(vendorItem);

                   await StoreManager.DocumentStore.OfflineUploadSync();
               }

               quotation.Status = QuotationStatus.done.ToString();

               quotation.DateSigned = DateTime.Now;

               quotation.SaleOrder.ToSend = true;

               await StoreManager.SaleOrderStore.UpdateAsync(quotation.SaleOrder);

               Dialog.HideLoading();

               await CoreMethods.DisplayAlert("Alerte","Email has been sent","OK");

               await CoreMethods.PopPageModel();

           }
           else
           {
               await CoreMethods.DisplayAlert("Alert", "Please sign the quotation first.", "Ok");
           }

       });

       

        public Command ConditionTapped => new Command(() =>
       {
           TermsConditionSelected = !TermsConditionSelected;
       });

        string conditionimage;
        public string ConditionImageSource
        {
            get{ return conditionimage; }
            set
            {
                conditionimage = value;

                RaisePropertyChanged();
            }
        }

        public List<string> PaymentSource { get; set; } = new List<string> { PaymentMethod.None.ToString(), PaymentMethod.Cash.ToString(), PaymentMethod.CreditCard.ToString(), PaymentMethod.DebitCard.ToString() };

        string selecteditem;
        public string SelectedItem
        {
            get { return selecteditem; }
            set
            {
                selecteditem = value;

                if(!string.IsNullOrEmpty(selecteditem))
                    Quotation.PaymentMethod = ParseEnum<PaymentMethod>(selecteditem);

                RaisePropertyChanged();
            }
        }

        string conditionstext;
        public string ConditionsText
        {
            get { return conditionstext; }
            set
            {
                conditionstext = value;

                RaisePropertyChanged();
            }
        }

        string notetext;
        public string NoteText 
        {
            get{ return notetext; }
            set
            {
                notetext = value;

                quotation.PaymentNotes = notetext;

                RaisePropertyChanged();
            }
        }

        double amount;
        public double Amount
        {
            get { return amount; }
            set
            {
                amount = value;

                RaisePropertyChanged();
            }
        }


        byte[] signimage;
        public byte[] SignImage
        {
            get { return signimage; }
            set
            {
                signimage = value;

                RaisePropertyChanged();
            }
        }

        bool issigned;
        public bool IsSigned
        {
            get { return issigned; }
            set
            {
                issigned = value;

                quotation.IsSignedValidated = value;

                RaisePropertyChanged();
            }
        }


        bool termsconditionselected;
        public bool TermsConditionSelected
        {
            get{ return termsconditionselected; }
            set
            {
                termsconditionselected = value;

                if (!termsconditionselected)
                {
                    ButtonColor = Color.FromHex("d9d9d9");
                    ConditionImageSource = "";
                }
                else
                {
                    ButtonColor = Color.FromRgb(47, 170, 195);
                    ConditionImageSource = "check2.png";
                }

                quotation.IsConditionsAgree = value;
              
                RaisePropertyChanged();
            }
        }

        private List<ProductQuotationModel> Orderitems { get; set; }


        Color buttoncolor;
        public Color ButtonColor
        {
            get { return buttoncolor; }
            set
            {
                buttoncolor = value;

                RaisePropertyChanged();
            }
        }


        QuotationsModel quotation;
        public QuotationsModel Quotation
        {
            get { return quotation; }
            set
            {
                quotation = value;

                Title = "Order "+ quotation.Ref;

                Amount = quotation.TotalAmount;

                SelectedItem = quotation.SaleOrder.PaymentMethod;

                TermsConditionSelected = quotation.IsConditionsAgree;

                SignImage = quotation.SignedImage;

                //NoteText = quotation.SaleOrder.PaymentNote;

                string cnd = "";

                foreach (var item in quotation.TermsConditions)
                {
                    cnd += "- " + item + Environment.NewLine;
                }

                ConditionsText = cnd;

                IsSigned = quotation.IsSignedValidated;

                RaisePropertyChanged();
            }
        }



        string title;
        public string Title
        {
            get{ return title; }
            set
            {
                title = value;

                RaisePropertyChanged();
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            var context = initData as Tuple<QuotationsModel,Partner,List<ProductQuotationModel>>;

            if (context == null)
                return;

            Quotation = context.Item1;
            Customer = context.Item2;
            Orderitems = context.Item3;
        }

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

        string UnixTimeStamp()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp.ToString();
        }

	}
}
