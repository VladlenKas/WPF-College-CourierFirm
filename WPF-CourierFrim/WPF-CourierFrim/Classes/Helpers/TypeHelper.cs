using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CourierFrim.Classes.Helpers
{
    public static class TypeHelper
    {
        // Преобразовывает текст в дату либо возвращает 0
        public static DateOnly DateOnlyParse(string str)
        {
            try
            {
                DateOnly number = DateOnly.Parse(str);
                return number;
            }
            catch
            {
                return DateOnly.MinValue;
            }
        }

        // Преобразовывает текст в строку либо возвращает 0
        public static decimal DecemalParse(string str)
        {
            try
            {
                decimal number = Decimal.Parse(str);
                return number;
            }
            catch
            {
                return 0;
            }
        }

        // Преобразовывает текст в строку либо возвращает 0
        public static int IntParse(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                int number = Convert.ToInt32(str);
                return number;
            }
        }

        // Посчитывает возраст
        public static int CalculateAge(DateOnly birthDate)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - birthDate.Year;

            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
    }
}
