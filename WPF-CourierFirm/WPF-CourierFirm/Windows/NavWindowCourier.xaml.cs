using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.DialogWindows;
using WPF_CourierFirm.Model;
using WPF_CourierFirm.Pages.PagesCourier;
using WPF_CourierFirm.WindowsDialog;

namespace WPF_CourierFirm.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavWindowCourier.xaml
    /// </summary>
    public partial class NavWindowCourier : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _courier;

        // Конструктор
        public NavWindowCourier(Employee courier)
        {
            _dbContext = new();
            _courier = courier;

            InitializeComponent();
            Title = $"Меню Курьера. Сотрудник: {courier.Fullname}";
            FIcourier.Text = courier.FIname;

            UpdateVisibilityOrders();

            App.MenuWindow = this;
        }

        // Методы
        private void UpdateVisibilityOrders()
        {
            if (_courier.Transport != null)
            {
                orderRB.IsChecked = true;
                orderRB.IsEnabled = true;
                deliveryRB.IsEnabled = true;
            }
            else
            {
                statRB.IsChecked = true;
                orderRB.IsEnabled = false;
                deliveryRB.IsEnabled = false;
                MessageHelper.MessageAbsenceTransport();
            }
        }

        // Обработчики событий
        private void OrderRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new OrderPageCourier(_courier));
            titlePage.Text = "Заказы";
        }

        private void DeliveryRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new DeliveryPageCourier(_courier));
            titlePage.Text = "Доставки";
        }

        private void RateRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new RatePageCourier());
            titlePage.Text = "Тарифы";
        }

        private void StatisticRButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrentPage.Navigate(new StatisticPageCourier(_courier));
            titlePage.Text = "Статистика";
        }

        private void InfoTransportButtonButton_Click(object sender, RoutedEventArgs e)
        {
            AddTransportWindow window = new(_courier);
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.AddedTransport;
            if (saved)
            {
                MessageHelper.MessageAddedTransport();
                UpdateVisibilityOrders();
            }

            _dbContext.Attach(_courier);
        }

        private void ExitRButton_Checked(object sender, RoutedEventArgs e)
        {
            AuthWindow window = new();
            window.Show();
            Close();
        }
    }
}
