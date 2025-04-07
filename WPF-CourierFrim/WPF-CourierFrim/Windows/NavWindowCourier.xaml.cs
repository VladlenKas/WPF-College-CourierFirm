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
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Pages.PagesCourier;

namespace WPF_CourierFrim.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavWindowCourier.xaml
    /// </summary>
    public partial class NavWindowCourier : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public NavWindowCourier(Employee employee)
        {
            _dbContext = new();
            _thisEmpoyee = employee;

            InitializeComponent();
            Title = $"Меню Курьера. Сотрудник: {employee.Fullname}";
            FIcourier.Text = employee.FIname;

            OrderRB.IsChecked = true;
            App.MenuWindow = this;
        }

        // Обработчики событий
        private void OrderRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new OrderPageCourier(_thisEmpoyee));
            titlePage.Text = "Заказы";
        }

        private void DeliveryRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new DeliveryPageCourier(_thisEmpoyee));
            titlePage.Text = "Доставки";
        }

        private void RateRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new RatePageCourier(_thisEmpoyee));
            titlePage.Text = "Тарифы";
        }

        private void StatisticRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new StatisticPageCourier(_thisEmpoyee));
            titlePage.Text = "Статистика";
        }

        private void ExitRButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthWindow window = new();
            window.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AuthWindow window = new();
            window.Show();
        }
    }
}
