using System;
using System.Collections.Generic;
using SignaturePad.Forms;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class ContractSignValidatePage : ContentPage
    {
        public ContractSignValidatePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            var context = BindingContext as ContractSignValidatePageModel;

            var img = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg,true,true);

            context.ImageStream = img;
        }

        void ClearSign(object sender, System.EventArgs e)
        {
            signaturePad.Clear();
        }

    }
}
