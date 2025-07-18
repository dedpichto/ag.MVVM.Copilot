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
