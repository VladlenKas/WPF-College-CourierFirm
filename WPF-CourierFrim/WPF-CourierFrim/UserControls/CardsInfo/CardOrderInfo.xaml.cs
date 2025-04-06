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
using System.Windows.Shapes;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.UserControls.CardsInfo
{
    /// <summary>
    /// Логика взаимодействия для CardOrderInfo.xaml
    /// </summary>
    public partial class CardOrderInfo : Window
    {
        // Поля и свойства
        public CourierServiceContext _dbContext;
        public Order _order;

        // Конструктор
        public CardOrderInfo(Order order)
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
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
