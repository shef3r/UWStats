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
using StatifyUWPLib;

namespace Statify
{
    public sealed partial class InterfacePage : Page
    {
        public string token;
        public InterfacePage()
        {
            this.InitializeComponent();
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

        //private async void Logout(object sender, RoutedEventArgs e)
        //{
        //    Auth.ClearCredentials();
        //}
    }
}
