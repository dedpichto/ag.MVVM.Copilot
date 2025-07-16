using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyApp.Services
{
    public class IconProvider : IIconProvider
    {
        public DrawingImage GetIcon(string iconName) => (DrawingImage)App.Current.FindResource(iconName);
    }
}
