using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyApp.Services
{
    public interface IIconProvider
    {
        DrawingImage GetIcon(string iconName);
    }
}
