using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Factories
{
    public interface IViewModelFactory<T>
    {
        T Create();
    }

}
