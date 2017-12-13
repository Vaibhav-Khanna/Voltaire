using System;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Collections.Generic;

namespace voltaire.PageModels
{
    public class MessagesPageModel : BasePageModel
    {


        public Command AddMessage => new Command((obj) =>
       {
            var message = new Message() { AuthorId = 0 , Date = DateTime.Now, Body = MessageText };
            Quotation.Messages.Add(message);
            MessageSource.Add(new MessageModel(message){ Index = messagesource.Count +1 });
            //MessageText = null;
       });

        public Command BackButton => new Command(async () =>
		{
			await CoreMethods.PopPageModel();
		});


        QuotationsModel Quotation { get; set; }


        ObservableCollection<MessageModel> messagesource;
        public ObservableCollection<MessageModel> MessageSource
        {
            get { return messagesource; }
            set
            {
                messagesource = value;
                RaisePropertyChanged();
            }
        }



        string messagetext;
        public string MessageText
        {
            get { return messagetext; }
            set
            {
                messagetext = value;
                RaisePropertyChanged();
            }
        }




        public override void Init(object initData)
        {
            base.Init(initData);

            Quotation = (initData as QuotationsModel);

            var message_list = Quotation.Messages;

            List<MessageModel> message_models = new List<MessageModel>();

            foreach (var item in message_list)
            {
                message_models.Add(new MessageModel(item){ Index = message_models.Count + 1});
            }

            MessageSource = new ObservableCollection<MessageModel>(message_models);

        }


    }
}
