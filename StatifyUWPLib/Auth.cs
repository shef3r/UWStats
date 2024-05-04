using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SpotifyAPI;
using SpotifyAPI.Web;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;
using Newtonsoft.Json;

using static StatifyUWPLib.SettingsProvider;

namespace StatifyUWPLib
{
    public class Auth
    {
        public static string clientID = "9eb749f187ca40bf911905d6c8b9f0b5";
        public static bool isAuthorized
        {
            get
            {
                return AccessToken != string.Empty;
            }
        }

        public static async Task GetCallback(string code)
        {
            (string verifier, string challenge) = VerifierAndChallenge;
            var initialResponse = await new OAuthClient().RequestToken(
              new PKCETokenRequest(Auth.clientID, code, new Uri("http://localhost:5543/callback"), verifier)
            );

            AccessToken = initialResponse.AccessToken;
            SettingsProvider.RefreshToken = initialResponse.RefreshToken;
        }

        public static async Task RefreshToken()
        {
            var newResponse = await new OAuthClient().RequestToken(
              new PKCETokenRefreshRequest(clientID, SettingsProvider.RefreshToken)
            );
            AccessToken = newResponse.AccessToken;
            SettingsProvider.RefreshToken = newResponse.RefreshToken;
        }

        public static (string, string) VerifierAndChallenge
        {
            get
            {
                if(PKCEVerifier != string.Empty && VerifierChallenge != string.Empty) return (PKCEVerifier, VerifierChallenge);

                (string verifier, string challenge) = PKCEUtil.GenerateCodes();
                PKCEVerifier = verifier;
                VerifierChallenge = challenge;
                return (verifier, challenge);
            }
        }
    }
}
