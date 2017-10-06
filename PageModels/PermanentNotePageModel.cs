using System;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using voltaire.Models;

namespace voltaire.PageModels
{
    public class PermanentNotePageModel : BasePageModel
    {
        public PermanentNotePageModel()
        {
        }

        public Command BackButton => new Command(async(obj) =>
       {
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


        public override void Init(object initData)
        {
            base.Init(initData);

            Quotation = initData as QuotationsModel;

            NoteText = Quotation.PermanentNote;

        }

    }
}
