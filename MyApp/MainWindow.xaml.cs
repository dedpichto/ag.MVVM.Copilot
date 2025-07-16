using Microsoft.Extensions.DependencyInjection;
using MyApp.Commands;
using MyApp.Factories;
using MyApp.Services;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TextBox TextBox1 => textBox1;

        public TextBox TextBox2 => textBox2;


        public MainWindow()
        {
            InitializeComponent();
            AppCommands.RegisterBindings(this);

            //#if DEBUG
            //            // Режим дизайнера — можно вернуть заглушку или ничего не делать
            //            if (DesignerProperties.GetIsInDesignMode(this))
            //            {
            //                DataContext = new StubMainViewModel();
            //                return;
            //            }
            //#endif

            //var factory = App.ServiceProvider.GetService<IViewModelFactory<MainViewModel>>();
            var bridge = new WindowBridgeService(this);
            var factory = App.ServiceProvider.GetService<MainViewModelFactory>();
            DataContext = factory?.Create();
            (DataContext as MainViewModel)?.AttachWindowBridge(bridge);
        }

        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    base.OnSourceInitialized(e);

        //    var handle = new WindowInteropHelper(this).Handle;
        //    (DataContext as MainViewModel)?.SetWindowHandle(handle);
        //}

        

    }
}
