using System;
using System.Collections.Generic;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationSignPageModel : BasePageModel
    {

        public Command BackButton => new Command((obj) =>
       {
             CoreMethods.PopPageModel();
       });

        public Command SignValidate => new Command( (obj) =>
       {
           if (!TermsConditionSelected)
           {
			   CoreMethods.DisplayAlert("Alert", "Please accept the terms and conditions first in order to validate the quotation.", "Ok");
               return;
           }

            if(IsSigned)
            {
                CoreMethods.DisplayAlert("Alert","You have already signed this quotation !","Ok");
                return;
            }

            if (SignImage != null)
            {             
                quotation.SignedImage = SignImage; 
                IsSigned = true;
                quotation.Status = QuotationStatus.Sent;
                quotation.DateSigned = DateTime.Now;
                CoreMethods.PopPageModel();
            }
            else
            {
                CoreMethods.DisplayAlert("Alert", "Please sign the quotation first.", "Ok");
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

                Quotation.PaymentMethod = ParseEnum<PaymentMethod>(SelectedItem);

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

                SelectedItem = quotation.PaymentMethod.ToString();

                TermsConditionSelected = quotation.IsConditionsAgree;

                SignImage = quotation.SignedImage;

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

            var context = initData as QuotationsModel;

            if (context == null)
                return;

            Quotation = context;
        }

		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}

	}
}
