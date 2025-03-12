using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_College_CourierFirm.Commands;
using WPF_College_CourierFirm.Model;

namespace WPF_College_CourierFirm.ViewModels
{
    // Класс AuthViewModel для окна авторизации
    public class AuthViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private readonly CourierServiceContext _dbContext = new();

        public ICommand ExitCommand { get; } // Команда для выхода из приложения
        public ICommand AuthCommand { get; } // Команда для входа в приложение

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(nameof(Login)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public AuthViewModel()
        {
            ExitCommand = new RelayCommand(ExitApp);
            AuthCommand = new RelayCommand(Auth);
        }

        // Метод для выхода из приложения
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        // Метод для проверки данных (логина и пароля)
        private Employee? Validate()
        {
            _dbContext.Employees.Include(r => r.Position).Load();

            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            var employee = _dbContext.Employees.SingleOrDefault(r => r.Login == Login && r.Password == Password);
            if (employee == null)
            {
                var userExists = _dbContext.Employees.Any(r => r.Login == Login);
                if (!userExists)
                {
                    MessageBox.Show("Пользователя с такими данными не существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }

            MessageBox.Show($"Добро пожаловать, {employee.Fullname}\n" +
                $"Вы вошли как {employee.Position.Name}", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);
            return employee;
        }

        // Метод для входа в приложение
        private void Auth()
        {
            var employee = Validate();
            if (employee != null)
            {
                // Переход на следующее окно
            }
        }

        // Реализация INotifyPropertyChanged для обновления UI
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
