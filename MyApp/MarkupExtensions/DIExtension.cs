using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace MyApp.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(object))]
    public class DIExtension : MarkupExtension
    {
        public Type Type { get; set; }

        public DIExtension()
        {
            
        }

        public DIExtension(Type type)
        {
            Type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
#if DEBUG
            var isDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());
            if (isDesignMode)
            {
                // Возвращаем временный экземпляр типа для дизайнера
                return Activator.CreateInstance(Type);
            }
#endif

            return App.ServiceProvider.GetService(Type)
                   ?? throw new InvalidOperationException($"Can't resolve {Type} from DI container.");
        }
    }
}
