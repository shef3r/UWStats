using StatifyUWPLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Statify
{
    public sealed partial class AuthWebPage : Page
    {
        public AuthWebPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            authView.Source = (Uri)e.Parameter;
        }

        private async void authView_NavigationStarting(Microsoft.UI.Xaml.Controls.WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args)
        {
            if (args.Uri.StartsWith("http://localhost:5543/callback"))
            {
                string code = args.Uri.Split("?code=")[1];
                DataPackage pkg = new DataPackage();
                pkg.SetText(code);
                Clipboard.SetContent(pkg);
                await Auth.GetCallback(code);
                if (Auth.isAuthorized)
                {
                    this.Frame.Navigate(typeof(InterfacePage), Auth.AccessToken);
                }
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.Title = "Error";
                    dialog.Content = "We couldn't authenticate you.";
                    dialog.PrimaryButtonText = "OK";
                    dialog.PrimaryButtonCommand = null;
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
