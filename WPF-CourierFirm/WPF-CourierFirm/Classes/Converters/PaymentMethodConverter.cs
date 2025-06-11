using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_CourierFirm.Classes.Converters
{
    internal class PaymentMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is sbyte method)
            {
                if (method == 0)
                {
                    return "Наличный расчет";
                }
                else
                {
                    return "Оплата картой";
                }
            }
            else
            {
                return "Оплата не произведена";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
