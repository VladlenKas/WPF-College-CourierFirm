using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Converters
{
    internal class AvailabilityTransportConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Employee employee = (Employee)value;
                if (employee.PostId == 1) // Если админ
                {
                    return "-";
                }
                else if (employee.Transport == null) // Если нет у курьера
                {
                    return "Отсутствует!";
                }
                else // Если у курьера имеется
                {
                    return "Имеется";
                }
            }
            else
            {
                return "Ошибка преобразования";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
