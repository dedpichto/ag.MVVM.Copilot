using Microsoft.Xaml.Behaviors;
using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mvvm.Helpers
{
    public class DataGridBehavior:Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            System.Diagnostics.Debug.WriteLine($"Attached {AssociatedObject.Tag}");
            AssociatedObject.Loaded += onDataGridLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            System.Diagnostics.Debug.WriteLine($"Detached {AssociatedObject.Tag}");
            AssociatedObject.Loaded -= onDataGridLoaded;
        }

        private void onDataGridLoaded(object sender, EventArgs e) {
            System.Diagnostics.Debug.WriteLine($"Loaded {AssociatedObject.Tag}");
            var viewModel = AssociatedObject.DataContext as BasicViewModelWithDataGrid;
            if (viewModel != null)
            {
                viewModel.ReplaseColumnsAction = (gridTag, columns) =>
                {
                    var tag = AssociatedObject.Tag;
                    System.Diagnostics.Debug.WriteLine($"In replace {AssociatedObject.Tag}");
                    if (tag is ExtMarket extTag && extTag == gridTag)
                    {
                        AssociatedObject.Columns.Clear();
                        foreach (var col in columns)
                            AssociatedObject.Columns.Add(col);
                    }
                };
                viewModel.ClearColumns = (gridTag) =>
                {
                    var tag = AssociatedObject.Tag;
                    System.Diagnostics.Debug.WriteLine($"In clear {AssociatedObject.Tag}");
                    if (tag is ExtMarket extTag && extTag == gridTag)
                    {
                        AssociatedObject.Columns.Clear();
                    }
                };
            }
        }
    }
}
