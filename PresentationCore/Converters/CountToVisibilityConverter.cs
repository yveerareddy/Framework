using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace PresentationCore.Converters
{
    public class CountToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable list = value as IEnumerable;

            if (list != null)
            {
                var enumerator = list.GetEnumerator();

                if (enumerator.MoveNext())
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
