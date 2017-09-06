using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationNotesPageModel : BasePageModel
    {

        public Command BackButton => new Command( async() =>
       {
            await CoreMethods.PopPageModel();
       });


        QuotationsModel quotation;
        public QuotationsModel Quotation
        {
            get { return quotation; }
            set
            {
                quotation = value;

                if (quotation.Notes == null)
                    quotation.Notes = new List<Note>();

                quotation.Notes.Add(new Note() { Date = DateTime.Now, id = 1, IsReminderActive = false, Publisher = "Vaibhav Khanna", Text = "hello world this a sample note" });

                var list = new List<NoteModel>();

                foreach (var item in quotation.Notes)
                {
                    list.Add(new NoteModel(item));
                }

                NoteSource = new ObservableCollection<NoteModel>(list);

                RaisePropertyChanged();
            }
        }

        ObservableCollection<NoteModel> notesource;
        public ObservableCollection<NoteModel> NoteSource
        {
            get { return notesource; }
            set
            {
                notesource = value;
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

    }
}
