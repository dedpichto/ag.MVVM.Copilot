using CommandImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModels
{
    public interface IViewModel:INotifyPropertyChanged
    {
        bool CommandCanExecute(IUICommand command);
        void CommandExecuted(IUICommand command);
    }
}
