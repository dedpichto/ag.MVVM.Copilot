using MyApp.Commands;
using MyApp.Helpers;
using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyApp.ViewModels
{
    public class MainViewModel : ViewModelBase

    {
        //public ICommand WindowLoadedCommand => new RelayCommand(OnLoaded);

        //private void OnLoaded()
        //{
        //    // твоя логика загрузки окна
        //}

        public ObservableCollection<TreeItem> TreeItems { get; } = new();

        private void OnClosed(object sender, EventArgs e)
        {
            ReleaseCommands();
        }

        private void OnLoaded(object sender, EventArgs e)
        {
            TreeItems.Add(new TreeItem
            {
                Name = "Root 1",
                Children =
                {
                    new TreeItem { Name = "Child 1.1" },
                    new TreeItem { Name = "Child 1.2" }
                }
            });
            TreeItems.Add(
            new TreeItem
            {
                Name = "Root 2",
                Children =
                {
                    new TreeItem { Name = "Child 2.1" },
                    new TreeItem { Name = "Child 2.2" }
                }
            });
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            // Логика с использованием handle
        }

        private IWindowBridge _windowBridge;
        private readonly IFocusService _focus;

        private readonly ITabOrderService _tab;

        private TreeItem _selectedTreeItem;


        public TreeItem SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                if (_selectedTreeItem == value) return;
                _selectedTreeItem = value;
                OnPropertyChanged();
            }
        }

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
            var props = GetType().GetProperties().Where(p => p.PropertyType == typeof(IUICommand)).ToArray();
            foreach (var prop in props)
            {
                var command = prop.GetValue(this) as IUICommand;
                if (command != null)
                {
                    Commands.Add(command);
                }
            }
        }

        public void AttachWindowBridge(IWindowBridge bridge)
        {
            _windowBridge = bridge;
            _windowBridge.Loaded += OnLoaded;
            _windowBridge.SourceInitialized += OnSourceInitialized;
            _windowBridge.Closed += OnClosed;
        }

        //public void SetWindowHandle(IntPtr handle)
        //{
        //    // использовать handle для подключения по pipe
        //}


        public override bool CommandCanExecute(IUICommand command)
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

        public override void CommandExecuted(IUICommand command)
        {
            switch (command.Name)
            {
                case "Hello":
                    MessageBox.Show($"Hello, {UserName}!");
                    break;
                case "FocusSecond":
                    FocusCommand.Execute("textBox2");
                    break;
                case "SwapTab":
                    FocusCommand.Execute("textBox1");
                    break;
            }
        }

        public FocusCommand FocusCommand { get; } = new FocusCommand();
    }

    public class TreeItem
    {
        public string Name { get; set; }
        public ObservableCollection<TreeItem> Children { get; set; } = new();

        // Можно добавить IsExpanded, IsSelected, Icon и т.д.
    }

}