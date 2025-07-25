using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyApp.Helpers
{
    // Focus Manager Attached Behavior
    public static class FocusManager
    {
        // Attached property for registering focus targets
        public static readonly DependencyProperty FocusTargetProperty =
            DependencyProperty.RegisterAttached(
                "FocusTarget",
                typeof(string),
                typeof(FocusManager),
                new PropertyMetadata(null, OnFocusTargetChanged));

        // Attached property for the focus command
        public static readonly DependencyProperty FocusCommandProperty =
            DependencyProperty.RegisterAttached(
                "FocusCommand",
                typeof(ICommand),
                typeof(FocusManager),
                new PropertyMetadata(null, OnFocusCommandChanged));

        // Dictionary to store weak references to focus targets
        private static readonly Dictionary<string, WeakReference> _focusTargets = new Dictionary<string, WeakReference>();

        // Getter for FocusTarget attached property
        public static string GetFocusTarget(DependencyObject obj)
        {
            return (string)obj.GetValue(FocusTargetProperty);
        }

        // Setter for FocusTarget attached property
        public static void SetFocusTarget(DependencyObject obj, string value)
        {
            obj.SetValue(FocusTargetProperty, value);
        }

        // Getter for FocusCommand attached property
        public static ICommand GetFocusCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(FocusCommandProperty);
        }

        // Setter for FocusCommand attached property
        public static void SetFocusCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(FocusCommandProperty, value);
        }

        // Callback when FocusTarget property changes
        private static void OnFocusTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element && e.NewValue is string targetName && !string.IsNullOrEmpty(targetName))
            {
                // Store weak reference to the element
                _focusTargets[targetName] = new WeakReference(element);
            }
        }

        // Callback when FocusCommand property changes
        private static void OnFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                // Clean up old command
                if (e.OldValue is ICommand)
                {
                    element.Loaded -= OnElementLoaded;
                }

                // Setup new command
                if (e.NewValue is ICommand newCommand)
                {
                    element.Loaded += OnElementLoaded;

                    // If element is already loaded, setup immediately
                    if (element.IsLoaded)
                    {
                        SetupFocusCommand(element, newCommand);
                    }
                }
            }
        }

        // Handle element loaded event
        private static void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var command = GetFocusCommand(element);
                if (command != null)
                {
                    SetupFocusCommand(element, command);
                }
            }
        }

        // Setup the focus command with the actual focus action
        private static void SetupFocusCommand(FrameworkElement element, ICommand command)
        {
            if (command is FocusCommand focusCommand)
            {
                focusCommand.SetFocusAction((targetName) =>
                {
                    if (_focusTargets.TryGetValue(targetName, out var weakRef) &&
                        weakRef.Target is UIElement targetElement)
                    {
                        // Use dispatcher to ensure focus is set on UI thread
                        targetElement.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            targetElement.Focus();

                            // Special handling for TextBox to select all text
                            if (targetElement is TextBox textBox)
                            {
                                textBox.SelectAll();
                            }
                        }), DispatcherPriority.Input);
                    }
                });
            }
        }
    }

    // Custom command implementation for focus management
    public class FocusCommand : ICommand
    {
        private Action<string> _focusAction;

        // Set the action that will be called when Execute is invoked
        public void SetFocusAction(Action<string> focusAction)
        {
            _focusAction = focusAction;
        }

        // Always return true - focus commands should always be executable
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Execute the focus action with the provided target name
        public void Execute(object parameter)
        {
            if (parameter is string targetName && !string.IsNullOrEmpty(targetName))
            {
                _focusAction?.Invoke(targetName);
            }
        }

        // Event required by ICommand interface
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
