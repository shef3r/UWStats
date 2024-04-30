using StatifyUWPLib;
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

namespace Statify
{
    public sealed partial class EntryPage : Page
    {
        public EntryPage()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string nav = (string)e.Parameter;
            string time = "short_term";

            if (nav.EndsWith("s")) { time = "short_term"; }
            else if (nav.EndsWith("m")) { time = "medium_term"; }
            else if (nav.EndsWith("l")) { time = "long_term"; }

            if (nav.StartsWith("a")) { showArtists(await Stats.artists(50, time)); }
            else if (nav.StartsWith("t")) {  } // no tracks yet
            else { await Stats.artists(50, time); }
        }

        private void showArtists(List<(string name, ImageSource image)> list)
        {
            foreach (var item in list)
            {
                stan.Text += item.name;
                stan.Text += " ";
            }
        }
    }
}
