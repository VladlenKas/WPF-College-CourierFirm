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
        #region Сообщения для уведомления

        // Предупреждение о пустых полях
        public static void MessageNullFields()
        {
            MessageBox.Show($"Заполните все поля!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о пустой или нулевой цене
        public static void MessageNullCost()
        {
            MessageBox.Show($"Цена должна быть больше 0!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о повторяющемся названии
        public static void MessageDuplicateName()
        {
            MessageBox.Show($"Такое название уже существует! Введите другое",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о повторяющемся описании
        public static void MessageDuplicateDescription()
        {
            MessageBox.Show($"Такое описание уже существует! Введите другое",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Сообщение о том, что изменения не были внесены
        public static void MessageNotChanges()
        {
            MessageBox.Show($"Вы не внесли изменений",
                "Отсутсвие изменений",
                MessageBoxButton.OK,
                MessageBoxImage.Question);
        }
        #endregion

        #region Сообщения подтверждения

        // Вызывает сообщение с подтверждением о выходе/закрытии окна
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

        // Подтверждение отмены заказа
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

        // Подтверждение принятия заказа
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

        // Подтверждение смены статуса
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

        // Подтверждение удаления
        public static bool ConfirmDelete()
        {
            var resultChanged = MessageBox.Show("Вы подтверждаете удаление?",
                "Предупреждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Удаление прошло успешно",
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

        // Подтверждение удаления
        public static bool ConfirmEdit()
        {
            var resultChanged = MessageBox.Show("Вы уверены, что заполнили все поля верно?",
                "Вопрос",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Изменение прошло успешно",
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
        #endregion
    }
}
