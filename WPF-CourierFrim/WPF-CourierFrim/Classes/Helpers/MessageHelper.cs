using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_CourierFrim.Classes.Helpers
{
    public static class MessageHelper
    {
        /// <summary>
        /// Вызывает сообщение с подтверждением о выходе/закрытии окна
        /// </summary>
        /// <returns></returns>
        public static void MessageNullFields()
        {
            MessageBox.Show($"Заполните все поля!", 
                "Предупреждение",
                MessageBoxButton.OK, 
                MessageBoxImage.Warning);
        }

        /// <summary>
        /// Вызывает сообщение с подтверждением о выходе/закрытии окна
        /// </summary>
        /// <returns></returns>
        public static void ConfirmExit(Window window)
        {
            var resultChanged = MessageBox.Show("Вы действительно хотите выйти?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                window.Close();
            }
        }

        /// <summary>
        /// Подтверждение отмены заказа
        /// </summary>
        /// <returns></returns>
        public static bool ConfirmChangeStatus()
        {
            var resultChanged = MessageBox.Show("Вы подтверждаете смену статуса?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Статус изменен",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return true; // удаляем
            }
            else
            {
                return false; // не удаляем
            }
        }

        /// <summary>
        /// Подтверждение принятия заказа
        /// </summary>
        /// <returns></returns>
        public static bool ConfirmAcceptOrder()
        {
            var resultChanged = MessageBox.Show("Вы подтверждаете принятие заказа?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Заказ принят и доступен для доставки",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return true; // да
            }
            else
            {
                return false; // нет
            }
        }

        /// <summary>
        /// Подтверждение смены статуса
        /// </summary>
        /// <returns></returns>
        public static bool ConfirmCancellationOrder()
        {
            var resultChanged = MessageBox.Show("Вы подтверждаете отмену заказа?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Заказ успешно отменен",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return true; // удаляем
            }
            else
            {
                return false; // не удаляем
            }
        }
    }
}
