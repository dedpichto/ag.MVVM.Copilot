using CommandImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface ICommandFactory
    {
        IUICommand Create(string key);
    }
}
