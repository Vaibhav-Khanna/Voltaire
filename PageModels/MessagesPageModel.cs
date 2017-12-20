using System;
using System.Collections.ObjectModel;
using voltaire.Models;
using voltaire.PageModels.Base;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace voltaire.PageModels
{
    public class MessagesPageModel : BasePageModel
    {


        public Command AddMessage => new Command(async (obj) =>
       {
           var currUser = await StoreManager.UserStore.GetCurrentUserAsync();
           // var message = new Message() { AuthorId = currUser.PartnerId, ExternalAuthorId = currUser.ExternalPartnerId, Date = DateTime.Now, Body = "<p>" + MessageText + "</p>", ResId = Quotation.SaleOrder.Id, MessageType = MessageType.comment, Model = "sale.order" };
           var message = new Message() { AuthorId = currUser.PartnerId, ExternalAuthorId = currUser.ExternalPartnerId, Date = DateTime.Now, Body = MessageText, ResId = Quotation.SaleOrder.Id, MessageType = MessageType.comment, Model = "sale.order" };

           //insertion de message dans la base
           var resul = await StoreManager.MessageStore.InsertAsync(message);

           Quotation.Messages.Add(message);

           MessageSource.Add(new MessageModel(message) { Index = MessageSource.Count + 1, Name = currUser.Name });
           MessageText = null;
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

        public async override void Init(object initData)
        {
            base.Init(initData);

            Quotation = (initData as QuotationsModel);

            //message recuperation from Quotation.SaleOrder.Id
            var message_list = await StoreManager.MessageStore.GetMessagesBySaleOrderIdAsync(Quotation.SaleOrder.Id);

            if (message_list != null)
            {
                if (message_list.Any())
                {
                    List<MessageModel> message_models = new List<MessageModel>();

                    foreach (var item in message_list)
                    {
                        //Name récupération
                        var partner = await StoreManager.CustomerStore.GetCustomerByMessageAuthorIdAsync(item.AuthorId);

                        message_models.Add(new MessageModel(item) { Expanded = true, Index = message_models.Count + 1, Name = partner.Name });
                    }
                    MessageSource = new ObservableCollection<MessageModel>(message_models);
                }
            }
            else
            {
                MessageSource = new ObservableCollection<MessageModel>();
            }


        }


    }
}
