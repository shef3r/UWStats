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
    public sealed partial class EntryPage : Page
    {
        public EntryPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string nav = (string)e.Parameter;
            string wiad = "";
            if (nav.StartsWith("a")) { wiad += "artists"; }
            else if (nav.StartsWith("t")) { wiad += "tracks"; }
            wiad += ", ";
            if (nav.EndsWith("s")) { wiad += "4 weeks"; }
            else if (nav.EndsWith("m")) { wiad += "6 months"; }
            else if (nav.EndsWith("l")) { wiad += "all time"; }

            stan.Text = wiad;
        }
    }
}
