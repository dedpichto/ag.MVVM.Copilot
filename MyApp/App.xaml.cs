using Microsoft.Extensions.DependencyInjection;
using MyApp.Factories;
using MyApp.Services;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        public static IServiceProvider ServiceProvider { get; private set; }


        //private IServiceProvider _services;

        protected override void OnStartup(StartupEventArgs e)

        {

            var collection = new ServiceCollection();



            collection.AddSingleton<IFocusService, FocusService>();

            collection.AddSingleton<ITabOrderService, TabOrderService>();

            collection.AddSingleton<IIconProvider, IconProvider>();

            collection.AddSingleton<ICommandFactory, CommandFactory>();
            collection.AddSingleton<MainViewModelFactory>();
            collection.AddTransient<MainViewModel>();

            collection.AddTransient<MainWindow>();



            ServiceProvider = collection.BuildServiceProvider();



            var window = ServiceProvider.GetRequiredService<MainWindow>();


            //collection.AddSingleton<IWindowBridge>(sp =>
            //{
            //    var mainWindow = window;
            //    return new WindowBridgeService(mainWindow);
            //});

            var focus = ServiceProvider.GetRequiredService<IFocusService>();

            var tab = ServiceProvider.GetRequiredService<ITabOrderService>();




            //window.Loaded += (sender, args) =>
            //{
            //    focus.Register("TextBox1", () => window.TextBox1.Focus());
            //    focus.Register("TextBox2", () => window.TextBox2.Focus());

            //    tab.Register("TextBox1", i => window.TextBox1.TabIndex = i);
            //    tab.Register("TextBox2", i => window.TextBox2.TabIndex = i);
            //};



            window.Show();

        }

    }
}

