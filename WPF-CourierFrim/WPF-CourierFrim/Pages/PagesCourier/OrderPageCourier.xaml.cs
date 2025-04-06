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
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls;
using static WPF_CourierFrim.UserControls.CardOrderAdmin;

namespace WPF_CourierFrim.Pages.PagesCourier
{
    /// <summary>
    /// Логика взаимодействия для OrderPageCourier.xaml
    /// </summary>
    public partial class OrderPageCourier : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public OrderPageCourier(Employee employee)
        {
            InitializeComponent();
            _dbContext = new();
            _thisEmpoyee = employee;

            // Загрузка комбобоксов и тд

            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();

            // Фильтрация и сортировка
            var orders = _dbContext.Orders
                        .Include(r => r.Rate)
                        .Include(r => r.Organisation)
                        .Include(r => r.Content)
                        .ThenInclude(r => r.ContentType)
                        .Where(r => r.DatetimeCompletion == null)
                        .ToList();

            cardsIC.Items.Clear();
            foreach (var order in orders)
            {
                var card = new CardOrderAdmin(order);
                card.DeleteOrderRequested += RemoveOrderRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void RemoveOrderRequested(object sender, OrderEventArgs e) => UpdateIC();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
