using System;
using System.Collections.Generic;
using System.IO;
using voltaire.DataStore;
using voltaire.DataStore.Implementation;
using voltaire.PageModels;
using Xamarin.Forms;

namespace voltaire.Pages
{
    public partial class QuotationSignPage : ContentPage
    {
        public QuotationSignPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
         
        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            var image_stream = await signaturePad.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg, false, true);

            var context = BindingContext as QuotationSignPageModel;

            if (image_stream == null || context == null)  // Check if signpad is note signed to avoid crash and execute command inside model
            {
                context.SignValidate.Execute(null);
                return;
            }

            context.SignImage = StoreManager.ReadFully(image_stream);

            context.SignValidate.Execute(null);
        }

        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    var context = BindingContext as QuotationSignPageModel;
        //    if (context == null)
        //        return;

        //    //if(context.IsSigned && context.SignImage!=null && SignContainer.Children.Contains(signaturePad)) //  if quotation is signed then replace signature pad with image of signature
        //    //{
        //    //    SignContainer.Children.Remove(signaturePad);

        //    //    var image = new Image {  VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromRgb(216,216,216) };	
        //    //    Stream stream = new MemoryStream(context.SignImage);
        //    //    image.Source = ImageSource.FromStream( () =>  stream);
        //    //    SignContainer.Children.Add(image);
        //    //}
        //}

    }
}
