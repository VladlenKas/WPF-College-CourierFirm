using ControlzEx.Standard;
using Microsoft.EntityFrameworkCore;
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
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Windows;

namespace WPF_CourierFrim
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private string Login => loginTB.Text;
        private string Password => PasswordHelper.GetPassword(PassPB, PassTB);

        // Конструктор
        public AuthWindow()
        {
            InitializeComponent();
            _dbContext = new();
        }

        // Методы
        private Employee? Authenticate(string login, string password)
        {
            _dbContext.Employees.Include(r => r.Post).Load();

            return _dbContext.Employees.SingleOrDefault(r =>
            r.Login == login && r.Password == password);
        }

        private void Auth()
        {
            if (Login == string.Empty || Password == string.Empty)
            {
                MessageHelper.NullFields();
                return;
            }

            var employee = Authenticate(Login, Password);

            if (employee == null)
            {
                // Если пользователь не найден
                MessageBox.Show("Пользователь с указанными данными не найден. Проверьте логин и пароль.",
                                "Ошибка авторизации",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            else
            {
                // Если пользователь найден
                MessageBox.Show($"Добро пожаловать, {employee.Fullname}!\nВаша должность: {employee.Post.Name}",
                                "Успешная авторизация",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                if (employee.PostId == 1)
                {
                    NavWindowAdmin navWindowAdmin = new(employee);
                    navWindowAdmin.Show();
                    Close();
                }
                else
                {
                    // Вход курьера
                }
            }
        }

        // Тригеры
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new();
            regWindow.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Auth();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) 
        {
            DialogHelper.ConfirmExit(this);
        }

        private void VisibilityPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordHelper.ToggleVisibility(sender, PassPB, PassTB);
        }
    }
}
