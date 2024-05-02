using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace StatifyUWPLib
{
    public class Track
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public ImageSource Cover { get; set; }
    }
}
