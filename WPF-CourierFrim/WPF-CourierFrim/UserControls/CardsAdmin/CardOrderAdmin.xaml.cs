using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Windows.WindowsInfo;

namespace WPF_CourierFrim.UserControls.CardsAdmin
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

            _order = order;
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
        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowOrder cardOrderInfo = new(_order);
            ComponentsHelper.ShowDialogWindowDark(cardOrderInfo);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool delete = MessageHelper.ConfirmCancellationOrder();
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
