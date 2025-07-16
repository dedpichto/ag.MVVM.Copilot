using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class FocusService : IFocusService

    {

        private readonly Dictionary<string, Action> _map = new();

        public void Register(string key, Action action) => _map[key] = action;

        public void Focus(string key)
        {
            if (_map.TryGetValue(key, out var action))
            {
                action?.Invoke();
            }
        }
    }
}
