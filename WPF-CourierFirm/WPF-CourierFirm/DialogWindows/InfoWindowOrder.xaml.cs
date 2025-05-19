using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPF_CourierFrim.WindowsInfo
{
    /// <summary>
    /// Логика взаимодействия для CardOrderInfo.xaml
    /// </summary>
    public partial class InfoWindowOrder : Window
    {
        // Поля и свойства
        public CourierServiceContext _dbContext;
        public Order _order;

        // Конструктор
        public InfoWindowOrder(Order order)
        {
            InitializeComponent();

            _order = order;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_order);

            var delivery = _dbContext.Deliveries.SingleOrDefault(d => d.OrderId == _order.OrderId);
            if (delivery != null)
            {
                numberDeliveryTB.Text = delivery.DeliveryId.ToString();
                statusDeliveryTB.Text = delivery.StatusDelivery.Name;


                if (delivery.EmployeeDeliveries.Count != 0)
                {
                    var employeeDelivery = delivery.EmployeeDeliveries.First(ed => ed.DeliveryId == delivery.DeliveryId);
                    courierFullnameTB.Text = employeeDelivery.Employee.Fullname;
                    courierPhoneTB.Text = $"+{employeeDelivery.Employee.Phone}";
                }
                else
                {
                    datetimeCompletedTB.Text = "Время отмены";
                    courierFullnameTB.Text = "Отстутствует";
                    courierPhoneTB.Text = "Отстутствует";
                }

            }
            else
            {
                statusDeliveryTB.Text = "Заказ еще готовится";
                numberDeliveryTB.Text = "Еще не принят курьером";
                courierFullnameTB.Text = "Еще не назначен курьер";
                courierPhoneTB.Text = "Еще не назначен курьер";
            }

            // Обновление привязок
            DataContext = _order;
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
