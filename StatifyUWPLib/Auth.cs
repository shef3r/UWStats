using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI;
using SpotifyAPI.Web;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml;

namespace StatifyUWPLib
{
    public class Auth
    {
        public static string clientID = "9eb749f187ca40bf911905d6c8b9f0b5";
        public static bool isAuthorized
        {
            get
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["accessToken"] != null) { return true; }
                return false;
            }
        }

        public static string AccessToken
        {
            get
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["accessToken"] != null) { return Windows.Storage.ApplicationData.Current.LocalSettings.Values["accessToken"].ToString(); }
                else
                {
                    throw new Exception("Token unavialable. Authenticate first.");
                }
            }
        }

        public static async Task GetCallback(string code)
        {
            DataPackage pkg = new DataPackage();
            pkg.SetText(code);
            Clipboard.SetContent(pkg);
            (string verifier, string challenge) = VerifierAndChallenge;
            pkg.SetText("COPIED:" + verifier);
            var initialResponse = await new OAuthClient().RequestToken(
              new PKCETokenRequest(Auth.clientID, code, new Uri("http://localhost:5543/callback"), verifier)
            );

            Windows.Storage.ApplicationData.Current.LocalSettings.Values["accessToken"] = initialResponse.AccessToken;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["refreshToken"] = initialResponse.RefreshToken;
        }

        public static async Task RefreshToken()
        {
            var newResponse = await new OAuthClient().RequestToken(
              new PKCETokenRefreshRequest(clientID, Windows.Storage.ApplicationData.Current.LocalSettings.Values["refreshToken"].ToString())
            );
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["accessToken"] = newResponse.AccessToken;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["refreshToken"] = newResponse.RefreshToken;
        }

        public static (string, string) VerifierAndChallenge
        {
            get
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["verifier"] != null && Windows.Storage.ApplicationData.Current.LocalSettings.Values["challenge"] != null)
                {
                    return (Windows.Storage.ApplicationData.Current.LocalSettings.Values["verifier"].ToString(), Windows.Storage.ApplicationData.Current.LocalSettings.Values["challenge"].ToString());
                }
                else
                {
                    (string verifier, string challenge) = PKCEUtil.GenerateCodes();
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["verifier"] = verifier;
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["challenge"] = challenge;
                    return (verifier, challenge);
                }
            }
        }
    }
}
