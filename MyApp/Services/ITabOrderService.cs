using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public interface ITabOrderService

    {

        void Register(string key, Action<int> setter);

        void SetTabIndex(string key, int value);

    }
}
