using MyApp.Commands;
using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyApp.ViewModels
{
    public class MainViewModel

    {
        //public ICommand WindowLoadedCommand => new RelayCommand(OnLoaded);

        //private void OnLoaded()
        //{
        //    // твоя логика загрузки окна
        //}

        private void OnLoaded(object sender, EventArgs e)
        {
            // Логика загрузки
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            // Логика с использованием handle
        }

        private IWindowBridge _windowBridge;
        private readonly IFocusService _focus;

        private readonly ITabOrderService _tab;



        public string UserName { get; set; }



        public IUICommand SayHelloCommand { get; }

        public IUICommand FocusSecondCommand { get; }

        public IUICommand SwapTabOrderCommand { get; }

        public MainViewModel()
        {

        }

        internal MainViewModel(ICommandFactory factory,
            IFocusService focus,
            ITabOrderService tab)

        {
            _focus = focus;
            _tab = tab;

            UserName = "World";

            AppCommands.Initialize(this);
            SayHelloCommand = AppCommands.SayHello;
            FocusSecondCommand = AppCommands.FocusSecond;
            SwapTabOrderCommand = AppCommands.SwapTab;
        }

        public void AttachWindowBridge(IWindowBridge bridge)
        {
            _windowBridge = bridge;
            _windowBridge.Loaded += OnLoaded;
            _windowBridge.SourceInitialized += OnSourceInitialized;
        }

        //public void SetWindowHandle(IntPtr handle)
        //{
        //    // использовать handle для подключения по pipe
        //}


        internal bool CommandCanExecute(IUICommand command)
        {
            switch (command.Name)
            {
                case "Hello":
                    return !string.IsNullOrWhiteSpace(UserName);
                case "FocusSecond":
                    return true;
                case "SwapTab":
                    return true;
                default:
                    return false;
            }
        }

        internal void CommandExecuted(IUICommand command)
        {
            switch (command.Name)
            {
                case "Hello":
                    MessageBox.Show($"Hello, {UserName}!");
                    break;
            }
        }
    }
}