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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls.CardsInfo;

namespace WPF_CourierFrim.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CardOrderAdmin.xaml
    /// </summary>
    public partial class CardOrderAdmin : UserControl
    {
        // Поля и свойства
        public Order Order { get; private set; }
        private CourierServiceContext _dbContext;
        private Order _order;

        // Конструктор
        public CardOrderAdmin(Order order)
        {
            InitializeComponent();
            _dbContext = new();
            _order = order;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();

            var order = _dbContext.Orders
                        .Include(r => r.Rate)
                        .Include(r => r.Organisation)
                        .Include(r => r.Content)
                        .ThenInclude(r => r.ContentType)
                        .Single(r => r.OrderId == _order.OrderId);

            _order = order;
            DataContext = _order;
        }

        // Обработчики событий
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            CardOrderInfo cardOrderInfo = new(_order);
            ComponentsHelper.DarkenWindow(cardOrderInfo);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool delete = MessageHelper.ConfirmDelete();
            if (!delete) return;

            _dbContext = new();
            OrderService.DeleteOrder(_order);

            DeleteOrderRequested?.Invoke(this, new OrderEventArgs { Order = this.Order }); // Уведомляем род. страницу об удалении
        }

        // События
        public event EventHandler<OrderEventArgs> DeleteOrderRequested;
        public class OrderEventArgs : EventArgs
        {
            public Order Order { get; set; }
        }
    }
}
