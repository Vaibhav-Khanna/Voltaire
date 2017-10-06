using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationInternalNotesPageModel : BasePageModel
    {

        public Command BackButton => new Command( async() =>
       {
            await CoreMethods.PopPageModel();
       });

        public Command AddNote => new Command(() =>
       {
           if (string.IsNullOrWhiteSpace(NoteText))
               return;

            var note = new Note(){ Date = DateTime.Now, id = quotation.InternalNotes.Count+1, IsReminderActive = false, Publisher = "Me", Text = NoteText };
            quotation.InternalNotes.Add(note);
            NoteSource.Add(new NoteModel(note){ CanEdit = this.CanEdit }); 
            NoteText = "";
       });


        QuotationsModel quotation;
        public QuotationsModel Quotation
        {
            get { return quotation; }
            set
            {
                quotation = value;

                CanEdit = quotation.Status == QuotationStatus.Sent ? false : true;

                if (quotation.InternalNotes == null)
                    quotation.InternalNotes = new List<Note>();

             
                var list = new List<NoteModel>();

                foreach (var item in quotation.InternalNotes)
                {
                    list.Add(new NoteModel(item){ CanEdit = this.CanEdit });
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

        string notetext;
        public string NoteText
        {
            get { return notetext; }
            set
            {
                notetext = value;
                RaisePropertyChanged();
            }
        }

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

            var context = initData as QuotationsModel;

            if (context == null)
            {
                var customer_context = initData as Customer;

                if (customer_context == null)
                    return;
            }
            else
            {
                Quotation = context;
            }


        }

    }
}
