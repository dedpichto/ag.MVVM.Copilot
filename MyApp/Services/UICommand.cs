using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyApp.Services
{
    public class UICommand<T> : IUICommand//, IDisposable
    {
        public string Name { get; }
        public string Text { get; }
        public string ToolTip { get; }
        public ImageSource Icon { get; }
        public KeyGesture HotKey { get; }
        public object CommandParameter { get; set; }

        public ICommand Command => _relayCommand;

        private RelayCommand<T> _relayCommand;
        private Action<T> _execute;
        private Predicate<T> _canExecute;


        public UICommand(string name, string text, string toolTip, ImageSource icon = null, KeyGesture hotKey = null, TimeSpan? requeryInterval = null)
        {
            Name = name;
            Text = text;
            ToolTip = toolTip;
            Icon = icon;
            HotKey = hotKey;

            var interval = requeryInterval ?? TimeSpan.FromMilliseconds(500);
            var dispatcherTimer = new DispatcherTimer
            {
                Interval = interval
            };

            dispatcherTimer.Tick += (s, e) =>
            {
                _relayCommand?.RaiseCanExecuteChanged();
            };

            dispatcherTimer.Start();

        }

        public UICommand(string text, string toolTip, ImageSource icon = null, KeyGesture hotKey = null, TimeSpan? requeryInterval = null)
        {
            Name = "name";
            Text = text;
            ToolTip = toolTip;
            Icon = icon;
            HotKey = hotKey;

            var interval = requeryInterval ?? TimeSpan.FromMilliseconds(500);
            var dispatcherTimer = new DispatcherTimer
            {
                Interval = interval
            };

            dispatcherTimer.Tick += (s, e) =>
            {
                _relayCommand?.RaiseCanExecuteChanged();
            };

            dispatcherTimer.Start();

        }

        public void Bind(Func<IUICommand, bool> canExecute, Action<IUICommand> execute)
        {
            _canExecute = param => canExecute(this);
            _execute = param => execute(this);
            _relayCommand = new RelayCommand<T>(_execute, _canExecute);
        }

        public void Executed(object parameter) => _execute?.Invoke((T)parameter);

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;

        //public void Dispose() => _canExecuteTimer?.Dispose();
    }

}
