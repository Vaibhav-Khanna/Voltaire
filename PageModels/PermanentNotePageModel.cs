using System;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Models;

namespace voltaire.PageModels
{
    public class PermanentNotePageModel : BasePageModel
    {
        

        public Command BackButton => new Command(async(obj) =>
       {
            StoreManager.SaleOrderStore.UpdateAsync(Quotation.SaleOrder);
            await CoreMethods.PopPageModel();
       });


        string notetext;
        public string NoteText 
        { 
            get { return notetext; }
            set
            {
                notetext = value;

                Quotation.PermanentNote = notetext;

                RaisePropertyChanged();
            }
        }

        public QuotationsModel Quotation { get; set; }

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


		public override void Init(object initData)
        {
            base.Init(initData);

            Quotation = initData as QuotationsModel;

            NoteText = Quotation.PermanentNote;

            CanEdit = Quotation.Status == QuotationStatus.sale.ToString() || Quotation.Status == QuotationStatus.done.ToString() ? false : true;
        }

    }
}
