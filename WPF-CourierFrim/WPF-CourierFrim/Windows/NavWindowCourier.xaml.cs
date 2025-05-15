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
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Pages.PagesCourier;
using WPF_CourierFrim.Windows.DialogWindows;

namespace WPF_CourierFrim.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavWindowCourier.xaml
    /// </summary>
    public partial class NavWindowCourier : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisCourier;

        // Конструктор
        public NavWindowCourier(Employee courier)
        {
            _dbContext = new();
            _thisCourier = courier;

            InitializeComponent();
            Title = $"Меню Курьера. Сотрудник: {courier.Fullname}";
            FIcourier.Text = courier.FIname;

            if (courier.Transport != null)
            {
                orderRB.IsChecked = true; 
            }
            else
            {
                statRB.IsChecked = true;
                orderRB.IsEnabled = false;
                MessageHelper.MessageAbsenceTransport();
            }

            App.MenuWindow = this;
        }

        // Обработчики событий
        private void OrderRButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_thisCourier.Transport != null)
            {
                CurrentPage.Navigate(new OrderPageCourier(_thisCourier));
                titlePage.Text = "Заказы"; 
            }
            else
            {
                MessageHelper.MessageAbsenceTransport();
            }
        }

        private void DeliveryRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new DeliveryPageCourier(_thisCourier));
            titlePage.Text = "Доставки";
        }

        private void RateRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new RatePageCourier());
            titlePage.Text = "Тарифы";
        }

        private void StatisticRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new StatisticPageCourier(_thisCourier));
            titlePage.Text = "Статистика";
        }

        private void InfoTransportButtonButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransportWindow window = new(_thisCourier);
            ComponentsHelper.ShowDialogWindowDark(window);

            _dbContext.Attach(_thisCourier);
        }

        private void ExitRButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthWindow window = new();
            window.Show();
            Close();
        }
    }
}
