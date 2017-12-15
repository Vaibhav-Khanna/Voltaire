using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

namespace voltaire.PopUps
{
    public static class ToastService
    {
        public static async Task Show(string text)
        {
            var popupPage = new ToastLayout(text);

            if(PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAllAsync();
            }

            await PopupNavigation.PushAsync(popupPage);
        }

        public static async Task Hide()
        {
            if (PopupNavigation.PopupStack.Count > 0)
            {
                await PopupNavigation.PopAllAsync();
            }
        }

    }
}
