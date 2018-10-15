// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace voltaire.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
        private const string LanguageKey = "language_key";
        private const string Delivery_Fee = "delivery_fee";

        private static readonly string SettingsDefault = string.Empty;

        #endregion

        //Settings settings;
        //public static Settings Current
        //{
        //    get { return settings ?? (settings = new Settings()); }
        //}

        const string DatabaseIdKey = "azure_database";
        static readonly int DatabaseIdDefault = 0;

        public static int DatabaseId
        {
            get { return AppSettings.GetValueOrDefault(DatabaseIdKey, DatabaseIdDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(DatabaseIdKey, value);
            }
        }

        public static int UpdateDatabaseId()
        {
            return DatabaseId++;
        }

        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static string DeviceLanguage
        {
            get
            {
                return AppSettings.GetValueOrDefault(LanguageKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LanguageKey, value);
            }
        }

        public static string DeliveryFee
        {
            get
            {
                return AppSettings.GetValueOrDefault(Delivery_Fee, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(Delivery_Fee, value);
            }
        }

    }
}