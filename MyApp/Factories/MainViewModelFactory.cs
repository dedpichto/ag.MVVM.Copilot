using Microsoft.Extensions.DependencyInjection;
using MyApp.Services;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Factories
{
    public class MainViewModelFactory : IViewModelFactory<MainViewModel>
    {
        private readonly IServiceProvider _sp;

        public MainViewModelFactory(IServiceProvider sp)
        {
            _sp = sp;
        }

        public MainViewModel Create()
        {
            var factory = _sp.GetRequiredService<ICommandFactory>();
            var focus = _sp.GetRequiredService<IFocusService>();
            var tab = _sp.GetRequiredService<ITabOrderService>();

            return new MainViewModel(factory, focus, tab);
        }
    }

}
