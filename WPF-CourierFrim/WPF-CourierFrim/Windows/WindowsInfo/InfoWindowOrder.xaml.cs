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

namespace WPF_CourierFrim.Windows.WindowsInfo
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
            DataContext = _order;
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
