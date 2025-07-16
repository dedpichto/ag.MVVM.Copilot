using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class TabOrderService : ITabOrderService
    {

        private readonly Dictionary<string, Action<int>> _map = new();

        public void Register(string key, Action<int> setter) => _map[key] = setter;

        public void SetTabIndex(string key, int value)
        {
            if (_map.TryGetValue(key, out var action))
            {
                action?.Invoke(value);
            }
        }

    }
}
