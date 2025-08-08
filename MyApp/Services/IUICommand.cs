using System;
using System.Windows.Input;
using System.Windows.Media;

namespace MyApp.CustomCommands
{
    public interface IUICommand : IDisposable,ICommand
    {
        string Name { get; }
        string Text { get; }
        string ToolTip { get; }
        ImageSource Icon { get; }
        KeyGesture HotKey { get; }
        object CommandParameter { get; set; }

    }
}
