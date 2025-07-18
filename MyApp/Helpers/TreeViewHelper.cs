using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyApp.Helpers
{
    public static class TreeViewHelper
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItem",
                typeof(object),
                typeof(TreeViewHelper),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static object GetSelectedItem(DependencyObject obj) =>
            obj.GetValue(SelectedItemProperty);

        public static void SetSelectedItem(DependencyObject obj, object value) =>
            obj.SetValue(SelectedItemProperty, value);

        public static readonly DependencyProperty MonitorSelectionProperty =
            DependencyProperty.RegisterAttached(
                "MonitorSelection",
                typeof(bool),
                typeof(TreeViewHelper),
                new PropertyMetadata(false, OnMonitorSelectionChanged));

        public static bool GetMonitorSelection(DependencyObject obj) =>
            (bool)obj.GetValue(MonitorSelectionProperty);

        public static void SetMonitorSelection(DependencyObject obj, bool value) =>
            obj.SetValue(MonitorSelectionProperty, value);

        private static void OnMonitorSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TreeView treeView && (bool)e.NewValue)
            {
                treeView.SelectedItemChanged += (s, args) =>
                {
                    SetSelectedItem(treeView, args.NewValue);
                };
            }
        }
    }

}
