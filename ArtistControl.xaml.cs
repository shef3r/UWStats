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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Statify
{
    public sealed partial class ArtistControl : UserControl
    {
        public ArtistControl()
        {
            this.InitializeComponent();
        }

        public string Position
        {
            get { return pos.Text; }
            set { pos.Text = "#" + value; }
        }

        public string Artist
        {
            get { return artistName.Text; }
            set { artistName.Text = value; }
        }

        public ImageSource Cover
        {
            get { return img.Source; }
            set { img.Source = value; }
        }
    }
}
