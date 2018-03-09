using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using voltaire.Models;
using voltaire.Models.DataObjects;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class QuotationInternalNotesPageModel : BasePageModel
    {

        private User currUser;

        public Command BackButton => new Command(async () =>
      {
          await CoreMethods.PopPageModel();
      });

        public Command AddNote => new Command(async (obj) =>
       {          
           if (string.IsNullOrWhiteSpace(MessageText))
           {
               return;
           }

           if (currUser == null)
           {
               await CoreMethods.DisplayAlert("Error", "Experienced internal error sending this message. Reopen the app to try sending the message", "Ok");
               return;
           }

           var _messageText = MessageText;

           MessageText = null;

           //déterminantion du model de message
           string modelMessage;
           string resId;

           if (Quotation != null)
           {
               modelMessage = "sale.order";
               resId = Quotation.SaleOrder.Id;
           }
           else
           {
               modelMessage = "res.partner";
               resId = Customer.Id;
           }

           var message = new Message() { AuthorId = currUser.PartnerId, ExternalAuthorId = currUser.ExternalPartnerId, Date = DateTime.Now, Body = _messageText, ResId = resId, MessageType = MessageType.comment.ToString(), Model = modelMessage };

           //insertion de message dans la base
           var resul = await StoreManager.MessageStore.InsertAsync(message);

           MessageSource.Add(new MessageModel(message) { Index = MessageSource.Count + 1, Name = currUser.Name });

       });

        QuotationsModel Quotation { get; set; }

        Partner Customer { get; set; }

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

            IsLoading = true;

            currUser = await StoreManager.UserStore.GetCurrentUserAsync();

            if (Quotation != null)
            {
                //message recuperation from Quotation.SaleOrder.Id
                var message_list = await StoreManager.MessageStore.GetMessagesByResIdAsync(Quotation.SaleOrder.Id, "sale.order");

                if (message_list != null)
                {
                    if (message_list.Any())
                    {
                        List<MessageModel> message_models = new List<MessageModel>();

                        foreach (var item in message_list)
                        {
                            //Name récupération
                            var partner = await StoreManager.CustomerStore.GetCustomerByMessageAuthorIdAsync(item.AuthorId);

                            message_models.Add(new MessageModel(item) { Index = message_models.Count + 1, Name = partner?.Name });
                        }
                        MessageSource = new ObservableCollection<MessageModel>(message_models);
                    }
                }
                else
                {
                    MessageSource = new ObservableCollection<MessageModel>();
                }
            }
            else
            {

                Customer = (initData as Partner);

                var message_list = await StoreManager.MessageStore.GetMessagesByResIdAsync(Customer.Id, "res.partner");

                if (message_list != null)
                {
                    if (message_list.Any())
                    {
                        List<MessageModel> message_models = new List<MessageModel>();

                        foreach (var item in message_list)
                        {
                            //Name récupération
                            var partner = await StoreManager.CustomerStore.GetCustomerByMessageAuthorIdAsync(item.AuthorId);

                            message_models.Add(new MessageModel(item) { Index = message_models.Count + 1, Name = partner?.Name });
                        }
                        MessageSource = new ObservableCollection<MessageModel>(message_models);
                    }
                }
                else
                {
                    MessageSource = new ObservableCollection<MessageModel>();
                }
            }

            IsLoading = false;

        }

    }
}
