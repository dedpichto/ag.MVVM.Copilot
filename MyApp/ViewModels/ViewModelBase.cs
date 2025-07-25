using MyApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModels
{
    public abstract class ViewModelBase : IViewModel
    {
        internal readonly List<IUICommand> Commands = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public abstract bool CommandCanExecute(IUICommand command);
        public abstract void CommandExecuted(IUICommand command);
        public void ReleaseCommands()
        {
            foreach (var command in Commands)
            {
                command.Dispose();
            }
        }
    }
}
