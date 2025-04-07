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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Windows.WindowsInfo;
using Microsoft.EntityFrameworkCore;
using WPF_CourierFrim.Classes.Helpers;

namespace WPF_CourierFrim.UserControls.CardsCourier
{
    /// <summary>
    /// Логика взаимодействия для CardOrderCourier.xaml
    /// </summary>
    public partial class CardOrderCourier : UserControl
    {
        // Поля 
        public Order Order { get; private set; }
        private CourierServiceContext _dbContext;
        private Order _order;
        private Employee _employee;

        // Конструктор
        public CardOrderCourier(Order order, Employee employee)
        {
            InitializeComponent();

            _order = order;
            _employee = employee;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_order);
            DataContext = _order;
        }

        // Обработчики событий
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowOrder cardOrderInfo = new(_order);
            ComponentsHelper.DarkenWindow(cardOrderInfo);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            bool accept = MessageHelper.ConfirmAcceptOrder();
            if (!accept) return;

            _dbContext = new();
            OrderService.AcceptOrderCourier(_order, _employee);

            AcceptOrderRequested?.Invoke(this, new OrderEventArgs { Order = this.Order }); // Уведомляем род. страницу об удалении
        }

        // События
        public event EventHandler<OrderEventArgs> AcceptOrderRequested;
        public class OrderEventArgs : EventArgs
        {
            public Order Order { get; set; }
        }
    }
}