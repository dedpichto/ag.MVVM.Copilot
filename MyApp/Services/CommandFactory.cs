using CommandImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MyApp.Services
{
    public class CommandFactory : ICommandFactory

    {
        private readonly IFocusService _focus;
        private readonly ITabOrderService _tab;
        private readonly IIconProvider _icons;

        public CommandFactory(IFocusService focus, ITabOrderService tab, IIconProvider icons)

        {
            _focus = focus;
            _tab = tab;
            _icons = icons;
        }
        
        public IUICommand Create(string key) => key switch
        {
            "Hello" => new UICommand("hello","Привет", "Поздороваться", _icons.GetIcon("HelloIcon"),new KeyGesture(Key.H, ModifierKeys.Control)),
            "FocusSecond" => new UICommand("Focus","Фокус на второе", "Перенести фокус"),
            "SwapTab" => new UICommand("Tab","Поменять TabIndex", "Изменить порядок"),
            _ => throw new ArgumentException("Неизвестная команда")
        };

    }
}
