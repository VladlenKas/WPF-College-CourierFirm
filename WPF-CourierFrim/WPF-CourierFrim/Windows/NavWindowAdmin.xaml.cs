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
using WPF_CourierFrim.Pages.PagesAdmin;

namespace WPF_CourierFrim.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavWindowAdmin.xaml
    /// </summary>
    public partial class NavWindowAdmin : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public NavWindowAdmin(Employee employee)
        {
            _dbContext = new();
            _thisEmpoyee = employee;

            InitializeComponent();
            Title = $"Меню Администратора. Сотрудник: {employee.Fullname}";
            FIcourier.Text = employee.FIname;

            orderRB.IsChecked = true;
            App.MenuWindow = this;
        }

        // Обработчики событий
        private void DeliveryRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new DeliveryPageAdmin());
            titlePage.Text = "Доставки";
        }

        private void OrderRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new OrderPageAdmin());
            titlePage.Text = "Заказы";
        }

        private void RateRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new RatePageAdmin());
            titlePage.Text = "Тарифы";
        }

        private void EmployeeRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new EmployeePageAdmin());
            titlePage.Text = "Сотрудники";
        }

        private void VehicleRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new TransportPageAdmin());
            titlePage.Text = "Автомобили";
        }

        private void OrganisationRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new OrganisationPageAdmin());
            titlePage.Text = "Организации";
        }

        private void StatisticRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new StatisticPageAdmin());
            titlePage.Text = "Статистика";
        }

        private void ExitRButton_Checked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AuthWindow window = new();
            window.Show();
        }
    }
}
