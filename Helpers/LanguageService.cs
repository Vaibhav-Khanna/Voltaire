using System;
using MeditSolution.Service;
using voltaire.Resources;
using Xamarin.Forms;

namespace voltaire.Helpers
{
    public class LanguageService
    {

		public void SetLanguage()
		{
			var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

			AppResources.Culture = ci;

			Settings.DeviceLanguage = ci.TwoLetterISOLanguageName == "fr" ? "fr" : "en";
           
			DependencyService.Get<ILocalize>().SetLocale(ci);
		}

    }
}
