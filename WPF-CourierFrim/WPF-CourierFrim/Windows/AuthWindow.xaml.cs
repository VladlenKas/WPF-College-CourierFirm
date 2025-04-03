using Microsoft.EntityFrameworkCore; // Работа с БД
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_CourierFrim.Classes; // Вспомогательные классы
using WPF_CourierFrim.Model; // Модели данных
using WPF_CourierFrim.Windows; // Окна приложения

namespace WPF_CourierFrim
{
    public partial class AuthWindow : Window // Окно авторизации
    {
        // Поля и свойства
        private CourierServiceContext _dbContext; // Контекст БД
        private string Login => loginTB.Text; // Логин из поля ввода
        private string Password => PasswordHelper.GetPassword(PassPB, PassTB); // Пароль из поля

        // Конструктор
        public AuthWindow()  // Инициализация окна
        {
            InitializeComponent();
            _dbContext = new();  // Создание контекста БД
        }

        private Employee? Authenticate(string login, string password)  // Аутентификация
        {
            _dbContext.Employees.Include(r => r.Post).Load();  // Загрузка данных о должностях

            return _dbContext.Employees.SingleOrDefault(r =>  // Поиск сотрудника
            r.Login == login && r.Password == password);
        }

        private void Auth()  // Процесс авторизации
        {
            if (Login == string.Empty || Password == string.Empty)  // Проверка пустых полей
            {
                MessageHelper.NullFields();  // Показать предупреждение
                return;
            }

            var employee = Authenticate(Login, Password);  // Поиск сотрудника

            if (employee == null)  // Если не найден
            {
                MessageBox.Show("Пользователь не найден",  // Сообщение об ошибке
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else  // Если найден
            {
                MessageBox.Show($"Добро пожаловать, {employee.Fullname}!\n" +
                                $"Ваша должность: {employee.Post.Name}",  // Приветствие
                                "Успех",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                if (employee.PostId == 1)  // Если админ
                {
                    NavWindowAdmin window = new(employee);  // Создать экземпляр класса окна
                    window.Show(); // Открыть окно админа
                    Close();  // Закрыть текущее окно
                }
                else  // Если курьер
                {
                    NavWindowCourier window = new(employee);  // Создать экземпляр класса окна
                    window.Show(); // Открыть окно курьера
                    Close(); // Закрыть текущее окно
                }
            }
        }

        // Обработчики событий
        private void Login_Click(object sender, RoutedEventArgs e)  // Кнопка "Войти"
        {
            Auth();  // Запустить логику авторизации
        }

        private void Exit_Click(object sender, RoutedEventArgs e)  // Кнопка "Выход"
        {
            DialogHelper.ConfirmExit(this);  // Подтверждение выхода
        }

        private void VisibilityPassword_Click(object sender, RoutedEventArgs e)  // Иконка глаза
        {
            PasswordHelper.ToggleVisibility(sender, PassPB, PassTB);  // Переключить видимость пароля
        }
    }
}
