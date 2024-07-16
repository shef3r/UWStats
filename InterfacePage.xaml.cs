using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWStatsUWPLib;
using Windows.ApplicationModel.Core;

using static UWStatsUWPLib.SettingsProvider;

namespace UWStats
{
    public sealed partial class InterfacePage : Page
    {
        public string token;
        public InterfacePage()
        {
            this.InitializeComponent();
            Window.Current.SetTitleBar(ttb);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            token = (string)e.Parameter;
        }

        private async void navview_Loaded(object sender, RoutedEventArgs e)
        {
            if (navview.SelectedItem == null) { navview.SelectedItem = navview.MenuItems.FirstOrDefault(); }
            pfp.ProfilePicture = await Images.GetProfilePic();
            mainFrame.Navigate(typeof(StatsPage), "a");
        }

        private void navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem != null)
            {
                mainFrame.Navigate(typeof(StatsPage), args.InvokedItemContainer.Tag.ToString());
            }
        }

        private async void logOut_Click(object sender, RoutedEventArgs e)
        {
            // Just incase:tm:
            AccessToken = null;
            RefreshToken = null;
            PKCEVerifier = null;
            VerifierChallenge = null;

            RemoveSetting("accessToken");
            RemoveSetting("refreshToken");
            RemoveSetting("verifier");
            RemoveSetting("challenge");
            await CoreApplication.RequestRestartAsync("Log out requested.");
        }
    }
}
