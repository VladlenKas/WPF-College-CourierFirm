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
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls;
using Microsoft.EntityFrameworkCore;
using WPF_CourierFrim.UserControls.CardsAdmin;
using static WPF_CourierFrim.UserControls.CardsAdmin.CardOrderAdmin;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для OrderPageAdmin.xaml
    /// </summary>
    public partial class OrderPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;

        // Конструктор
        public OrderPageAdmin()
        {
            InitializeComponent();
            _dbContext = new();

            // Загрузка комбобоксов и тд

            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var orders = _dbContext.Orders.ToList();

            // Фильтрация и сортировка 

            cardsIC.Items.Clear();
            foreach (var order in orders)
            {
                var card = new CardOrderAdmin(order);
                card.DeleteOrderRequested += DeleteOrderRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void DeleteOrderRequested(object sender, OrderEventArgs e) => UpdateIC();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
