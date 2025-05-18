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

        // Предупреждение о необходимости зарегистрировать авто
        public static void MessageAbsenceTransport()
        {
            MessageBox.Show($"Для того, чтобы начать принимать заказы, вам необходимо " +
                $"зарегистрировать свое транспортное средство. Заполните данные, нажав на кнопку в панели меню " +
                $"«Данные о ТС». После этого вы сможете выполнять заказы.",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Сообщение об успшеном добавлении ТС
        public static void MessageAddedTransport()
        {
            MessageBox.Show($"Транспортное средство успешно зарегистрировано! " +
                $"Теперь вам доступны заказы. Успешных доставок :).",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

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
        
        // Предупреждение о пустой дате
        public static void MessageNullDate()
        {
            MessageBox.Show($"Дата рождения должна соответствовать формату «dd.MM.yyyy»!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о пустых полях с датой (для отчета)
        public static void MessageNullDatesFields()
        {
            MessageBox.Show($"Заполните поля с датой в соответствие с форматом " +
                $"«dd.MM.yyyy»!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о пустом пути (для отчета)
        public static void MessageNullFilepath()
        {
            MessageBox.Show($"Выберите путь для сохранения документа! Для этого нажмите на кнопку " +
                $"«Путь к документу»",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о слишком старой дате (для отчета)
        public static void MessageIncorrectYear()
        {
            MessageBox.Show($"Отчет можно составить ТОЛЬКО за текущий или предыдущий год, а также" +
                $"дата начала или завершения отчета должна быть НЕ позднее текущей даты!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о слишком большом периоде между датами (для отчета)
        public static void LongDatePeriod()
        {
            MessageBox.Show($"Период между датами должен быть не более 3-ех месяцев",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о слишком большом периоде между датами (для отчета)
        public static void WrongStartingDatePoint()
        {
            MessageBox.Show($"Дата начала отчета не может быть больше даты завершения!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о молодом возрасте
        public static void MessageInappropriateAge()
        {
            MessageBox.Show($"Возраст сотрудника должен быть от 18 до 80 лет!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о пустом или нулевом весе
        public static void MessageNullWeight()
        {
            MessageBox.Show($"Вес должен быть больше 0 кг. и соответствовать формату в подсказке!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о пустой или нулевой цене
        public static void MessageIncorrectEmail()
        {
            MessageBox.Show($"Электронная почта должна содержать корректный вид",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком номере
        public static void MessageShortPhone()
        {
            MessageBox.Show($"Номер телефона должен содержать 11 цифр!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком номере
        public static void MessageShortPassport()
        {
            MessageBox.Show($"Серия и номера паспорта должны содержать 10 цифр!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком названии
        public static void MessageShortName()
        {
            MessageBox.Show($"Название должно содержать минимум 3 символа!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком ФИО
        public static void MessageShortFio()
        {
            MessageBox.Show($"Фамилия имя и отчество должны содержать минимум 3 символа!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о коротком названии модели ТС
        public static void MessageShortModel()
        {
            MessageBox.Show($"Модель транспортного средства должна содержать минимум 3 символа!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком названии марки ТС
        public static void MessageShortBrand()
        {
            MessageBox.Show($"Марка транспортного средства должна содержать минимум 3 символа!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о коротком названии марки ТС
        public static void MessageInvalidYear()
        {
            MessageBox.Show($"Год выпуска транспортного средства должен быть в промежутке " +
                $"от 1980 года до текущего включительно!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о коротком номере ТС
        public static void MessageInvalidLicensePlate()
        {
            MessageBox.Show($"Лицензионный номер транспортного средства должен содержать 6 символов и " +
                $"соответствовать формату в примере подсказки!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о коротком названии марки цвета
        public static void MessageShortColor()
        {
            MessageBox.Show($"Цвет транспортного средства должен содержать минимум 3 символа!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком номере
        public static void MessageShortAddress()
        {
            MessageBox.Show($"Адрес(-а) должен(-ы) содержать минимум 5 символов!",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о коротком номере
        public static void MessageShortContentOrder()
        {
            MessageBox.Show($"Содержимое заказа должно содержать минимум 10 символов!",
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

        // Предупреждение о повторяющемся адресе
        public static void MessageDuplicateAddress()
        {
            MessageBox.Show($"Такой адрес уже существует! Введите другой",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о повторяющемся телефоне
        public static void MessageDuplicatePhone()
        {
            MessageBox.Show($"Такой номер телефона уже существует! Введите другой",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о повторяющемся паспорте
        public static void MessageDuplicatePassport()
        {
            MessageBox.Show($"Такие серия и номер паспорта уже существуют! Введите другие",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Предупреждение о повторяющемся почте
        public static void MessageDuplicateEmail()
        {
            MessageBox.Show($"Такой адрес электронной почты уже существует! Введите другой",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
        
        // Предупреждение о повторяющемся почте
        public static void MessageDuplicateLogin()
        {
            MessageBox.Show($"Такой логин уже существует! Введите другой",
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
        
        // Предупреждение о повторяющемся описании
        public static void MessageDuplicateLicensePlate()
        {
            MessageBox.Show($"Такой лицензионный номер ТС уже существует! Введите другой",
                "Предупреждение",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        // Сообщение о том, что изменения не были внесены
        public static void MessageNotChanges()
        {
            MessageBox.Show($"Вы не внесли изменений",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Question);
        }
        #endregion

        #region Сообщения подтверждения

        // Предупреждение о том, что такой отчет уже существует
        public static bool MessageDuplicateFilpath()
        {
            var result = MessageBox.Show($"Отчет за текущий период уже существует! Вы уверены, что хотите" +
                $"перезаписать его?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        // Подтверждение изменения
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

                return true; 
            }
            else
            {
                return false; 
            }
        }

        // Подтверждение сохранения
        public static bool ConfirmSave()
        {
            var resultChanged = MessageBox.Show("Вы уверены, что заполнили все поля верно?",
                "Вопрос",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Добавление прошло успешно",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return true; 
            }
            else
            {
                return false; 
            }
        }

        // Подтверждение сохранения
        public static bool ConfirmSaveDocument()
        {
            var resultChanged = MessageBox.Show("Вы уверены, что заполнили все поля верно?",
                "Вопрос",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                MessageBox.Show("Сохранение документа прошло успешно",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return true; 
            }
            else
            {
                return false; 
            }
        }
        #endregion
    }
}
