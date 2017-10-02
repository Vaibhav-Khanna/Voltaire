using System;
using voltaire.PageModels.Base;
using Xamarin.Forms;

namespace voltaire.PageModels
{
    public class MessagesPageModel : BasePageModel
    {
        public MessagesPageModel()
        {
        }

		public Command BackButton => new Command(async () =>
		{
			await CoreMethods.PopPageModel();
		});




    }
}
