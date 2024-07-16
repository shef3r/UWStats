using Newtonsoft.Json;
using UWStatsUWPLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWStats
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
            else if (nav.StartsWith("t")) { showTracks(await Stats.tracks(50, time)); }
            else { await Stats.artists(50, time); }
        }

        private void showArtists(List<Artist> list)
        {
            int i = 1;
            foreach (Artist item in list)
            {
                EntryList.Children.Add(new ArtistControl() { Artist = item.Name, Cover = item.Image, Position = i.ToString() });
                i++;
            }
            ring.Visibility = Visibility.Collapsed;
        }

        private void showTracks(List<Track> list)
        {
            int i = 1;
            foreach (Track item in list)
            {
                EntryList.Children.Add(new TrackControl() { Title = item.Name, Artist = item.Artist, Album = item.Album, Cover = item.Cover, Position = i.ToString() });
                i++;
            }
            ring.Visibility = Visibility.Collapsed;
        }

    }
}
