using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MyApp.Services
{
    public interface IUICommand
    {
        string Name { get; }
        string Text { get; }
        string ToolTip { get; }
        ImageSource Icon { get; }
        ICommand Command { get; }
        KeyGesture HotKey { get; }
        object CommandParameter { get; set; }

        void Executed(object parameter);
        bool CanExecute(object parameter);
    }
}
