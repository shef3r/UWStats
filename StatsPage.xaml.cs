using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Statify
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatsPage : Page
    {
        public StatsPage()
        {
            this.InitializeComponent();
        }

        public string type;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            type = (string)e.Parameter;
        }

        private void timeview_Loaded(object sender, RoutedEventArgs e)
        {
            if (timeview.SelectedItem == null) { timeview.SelectedItem = timeview.MenuItems.FirstOrDefault(); }
            entryFrame.Navigate(typeof(EntryPage), type+"s");
        }

        private void timeview_ItemInvoked(Windows.UI.Xaml.Controls.NavigationView sender, Windows.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            entryFrame.Navigate(typeof(EntryPage), type + args.InvokedItemContainer.Tag);
        }
    }
}
