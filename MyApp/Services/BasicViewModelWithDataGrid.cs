using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mvvm.Helpers
{
    public enum ExtMarket
    {
        CapFloor,
        Swaptions,
        CpiVol
    }

    public class BasicViewModelWithDataGrid
    {
        public Action<ExtMarket, IEnumerable<DataGridColumn>> 
            ReplaseColumnsAction { get; set; }
        public Action<ExtMarket> ClearColumns { get;set; }
        
    }
}
