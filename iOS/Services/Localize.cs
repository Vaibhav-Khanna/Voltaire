﻿using System;
using System.Globalization;
using System.Threading;
using Foundation;
using MeditSolution.Service;
using voltaire.Resources;
using Xamarin.Forms;

[assembly: Dependency(typeof(voltaire.iOS.Services.Localize))]
namespace voltaire.iOS.Services
{
    
	public class Localize : ILocalize
	{
		public void SetLocale(CultureInfo ci)
		{
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		}

		public CultureInfo GetCurrentCultureInfo()
		{
			var netLanguage = "en";
			if (NSLocale.PreferredLanguages.Length > 0)
			{
				var pref = NSLocale.PreferredLanguages[0];
				netLanguage = iOSToDotnetLanguage(pref);
			}
			// this gets called a lot - try/catch can be expensive so consider caching or something
			System.Globalization.CultureInfo ci = null;
			try
			{
				ci = new System.Globalization.CultureInfo(netLanguage);
			}
			catch (CultureNotFoundException e1)
			{
				// iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
				// fallback to first characters, in this case "en"
				try
				{
					var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
					ci = new System.Globalization.CultureInfo(fallback);
				}
				catch (CultureNotFoundException e2)
				{
					// iOS language not valid .NET culture, falling back to English
					ci = new System.Globalization.CultureInfo("en");
				}
			}
			return ci;
		}

		string iOSToDotnetLanguage(string iOSLanguage)
		{
			var netLanguage = iOSLanguage;
			//certain languages need to be converted to CultureInfo equivalent
			switch (iOSLanguage)
			{
				case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
				case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
					netLanguage = "ms"; // closest supported
					break;
				case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
					netLanguage = "de-CH"; // closest supported
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}
			return netLanguage;
		}

		string ToDotnetFallbackLanguage(PlatformCulture platCulture)
		{
			var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually);
			switch (platCulture.LanguageCode)
			{
				case "pt":
					netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
					break;
				case "gsw":
					netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
					break;
					// add more application-specific cases here (if required)
					// ONLY use cultures that have been tested and known to work
			}
			return netLanguage;
		}
	}
}
