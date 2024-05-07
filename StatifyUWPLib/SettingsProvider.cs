using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace StatifyUWPLib
{
    public static class SettingsProvider
    {
        private static readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public static T GetSetting<T>(string key)
        {
            if (localSettings.Values.TryGetValue(key, out var value) && value is T)
            {
                return (T)value;
            }
            // We can't return null. Not all types are nullable, and we're using generics.
            return default;
        }

        public static void SetSetting<T>(string key, T value)
        {
            // We can always SET a value as it's automatically created, but not always read; hence the lack of null-checking.
            localSettings.Values[key] = value;
        }

        public static bool RemoveSetting(string key)
        {
            return localSettings.Values.ContainsKey(key) ? localSettings.Values.Remove(key) : false;
        }

        // Here are some shortcut properties to make things easier

        public static string AccessToken
        {
            get => GetSetting<string>("accessToken") ?? string.Empty;
            set => SetSetting("accessToken", value);
        }

        public static string RefreshToken
        {
            get => GetSetting<string>("refreshToken") ?? string.Empty;
            set => SetSetting("refreshToken", value);
        }

        public static string PKCEVerifier
        {
            get => GetSetting<string>("verifier") ?? string.Empty;
            set => SetSetting("verifier", value);
        }

        public static string ClientID
        {
            get => GetSetting<string>("clientID") ?? string.Empty;
            set => SetSetting("clientID", value);
        }

        public static string VerifierChallenge
        {
            get => GetSetting<string>("challenge") ?? string.Empty;
            set => SetSetting("challenge", value);
        }
    }
}