using MyApp.Services;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MyApp.Commands
{
    public static class AppCommands
    {
        public static readonly UICommand<string> SayHello;
        public static readonly UICommand<object> FocusSecond;
        public static readonly UICommand<object> SwapTab;

        static AppCommands()
        {
            SayHello = new UICommand<string>("Hello", "Say Hello", "Greets the user", (DrawingImage)App.Current.FindResource("HelloIcon"));
            FocusSecond = new UICommand<object>("FocusSecond", "Focus Second", "Focuses second control",(DrawingImage)App.Current.FindResource("FocusIcon"));
            SwapTab = new UICommand<object>("SwapTab", "Swap Tabs", "Swaps tab order", (DrawingImage)App.Current.FindResource("TabIcon"));
        }

        public static void Initialize(IViewModel context)
        {
            SayHello.Bind(
                (cmd) => context.CommandCanExecute(cmd),
                (cmd) => context.CommandExecuted(cmd));

            FocusSecond.Bind(
                (cmd) => context.CommandCanExecute(cmd),
                (cmd) => context.CommandExecuted(cmd));

            SwapTab.Bind(
                (cmd) => context.CommandCanExecute(cmd),
                (cmd) => context.CommandExecuted(cmd));
        }

        public static void RegisterBindings(Window window)
        {
            var commands = new List<IUICommand> { SayHello, FocusSecond, SwapTab };

            foreach (var cmd in commands)
            {
                if (cmd.HotKey is KeyGesture gesture)
                {
                    window.InputBindings.Add(new KeyBinding(cmd.Command, gesture));
                }
            }
        }
    }

}
