using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyApp.CustomCommands
{
    public class UICommand<T> : IUICommand
    {
        public string Name { get; }
        public string Text { get; }
        public string ToolTip { get; }
        public ImageSource Icon { get; }
        public KeyGesture HotKey { get; }
        public object CommandParameter { get; set; }


        private RelayCommand<T> _relayCommand;
        private Action<T> _executed;
        private Action<T> _execute;
        private Predicate<T> _canExecute;
        private readonly DispatcherTimer _canExecuteTimer;

        public event EventHandler CanExecuteChanged;

        public UICommand(string name, string text, string toolTip, ImageSource icon = null, KeyGesture hotKey = null, TimeSpan? requeryInterval = null)
        {
            Name = name;
            Text = text;
            ToolTip = toolTip;
            Icon = icon;
            HotKey = hotKey;

            var interval = requeryInterval ?? TimeSpan.FromMilliseconds(500);
            _canExecuteTimer = new DispatcherTimer
            {
                Interval = interval
            };

            _canExecuteTimer.Tick += (s, e) =>
            {
                _relayCommand?.RaiseCanExecuteChanged();
            };

            _canExecuteTimer.Start();

        }

        public UICommand(string text, string toolTip, ImageSource icon = null, KeyGesture hotKey = null, TimeSpan? requeryInterval = null)
        {
            Name = "name";
            Text = text;
            ToolTip = toolTip;
            Icon = icon;
            HotKey = hotKey;

            var interval = requeryInterval ?? TimeSpan.FromMilliseconds(500);
            _canExecuteTimer = new DispatcherTimer
            {
                Interval = interval
            };

            _canExecuteTimer.Tick += (s, e) =>
            {
                _relayCommand?.RaiseCanExecuteChanged();
            };

            _canExecuteTimer.Start();

        }

        public void Bind(Func<IUICommand, bool> canExecute, Action<IUICommand> executed)
        {
            _canExecute = param => canExecute(this);
            _executed = param => executed(this);
            _relayCommand = new RelayCommand<T>(_executed, _canExecute);
        }


        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;
        public void Dispose() => _canExecuteTimer?.Stop();
        public void Execute(object parameter) => _executed?.Invoke((T)parameter);

    }

}
