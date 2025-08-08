using MyApp.Services;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CommandImplementation
{
    public interface IUICommand : ICommand, IDisposable
    {
        string Name { get; }
        string Text { get; }
        string ToolTip { get; }
        ImageSource Icon { get; }
        KeyGesture HotKey { get; }
        object CommandParameter { get; set; }
        void Bind(Action<object> executed,
            Func<object, bool> canExecute);
    }

    public class UICommand : IUICommand
    {
        private  Action<object> _executed;
        private  Func<object, bool> _canExecute;
        private bool _isDisposed;
        private object _commandParameter;
        private readonly DispatcherTimer _timer;

        public string Name { get; }
        public string Text { get; }
        public string ToolTip { get; }
        public ImageSource Icon { get; }
        public KeyGesture HotKey { get; }

        public object CommandParameter
        {
            get => _commandParameter;
            set => _commandParameter = value;
        }

        public void Bind(Action<object> executed,
            Func<object, bool> canExecute)
        {
            _executed = executed ?? throw new ArgumentNullException(nameof(executed));
            _canExecute = canExecute;
        }

        public UICommand(
            string name = null,
            string text = null,
            string toolTip = null,
            ImageSource icon = null,
            KeyGesture hotKey = null)
        {
            Name = name ?? string.Empty;
            Text = text ?? string.Empty;
            ToolTip = toolTip ?? string.Empty;
            Icon = icon;
            HotKey = hotKey;
            _isDisposed = false;

            // Initialize DispatcherTimer to raise CanExecuteChanged every 500ms
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += (s, e) => RaiseCanExecuteChanged();
            _timer.Start();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (_isDisposed)
            {
                return false;
            }
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (!_isDisposed)
            {
                _executed(parameter);
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                // Stop and clean up the timer
                _timer?.Stop();
                // Clean up any resources
                if (Icon is IDisposable disposableIcon)
                {
                    disposableIcon.Dispose();
                }
            }
        }

        private void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
