using CommandImplementation;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CommandImplementation
{
    internal class AllCommands
    {
        public static void Initialize(IViewModel context)
        {
            var props = context.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(IUICommand))
                .ToArray();
            foreach (var prop in props)
            {
                if (prop.GetValue(context) is IUICommand cmd)
                {
                    cmd.Bind(
                        (obj) => context.CommandExecuted(cmd),
                        (obj) => context.CommandCanExecute(cmd)
                    );
                }
            }
        }

        public static IUICommand SayHello() => new UICommand(
            "Hello",
            "Say Hello",
            "Greets the user",
            (DrawingImage)Application.Current.FindResource("HelloIcon"),
            new KeyGesture(Key.H, ModifierKeys.Control));
        public static IUICommand FocusSecond() => new UICommand("FocusSecond", "Focus Second", "Focuses second control", (DrawingImage)Application.Current.FindResource("FocusIcon"));
        public static IUICommand SwapTab() => new UICommand("SwapTab", "Swap Tabs", "Swaps tab order", (DrawingImage)Application.Current.FindResource("TabIcon"));
    }
}
