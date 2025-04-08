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
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Windows.WindowsDialog
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        public int PaymentMethod => paymentCB.SelectedIndex;

        private CourierServiceContext _dbContext;
        private Delivery _delivery;

        // Конструктор
        public PaymentWindow(Delivery delivery)
        {
            InitializeComponent();
            _delivery = delivery;
            Saved = false;

            DataContext = _delivery;
            paymentCB.ItemsSource = new List<string> { "Наличный расчет", "Оплата картой" };
        }

        // Методы
        private bool CheckFields()
        {
            if (PaymentMethod == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool nullFields = CheckFields();
            if (nullFields)
            {
                MessageHelper.MessageNullFields();  // Показать предупреждение
                return;
            }

            bool accept = MessageHelper.ConfirmChangeStatus();
            if (!accept) return;

            DeliveryService.PaymentMethodDelivery(_delivery, PaymentMethod);
            Saved = true;
            Close();
        }
    }
}
