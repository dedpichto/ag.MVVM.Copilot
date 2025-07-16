using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface IFocusService

    {

        void Register(string key, Action focusAction);

        void Focus(string key);

    }
}
