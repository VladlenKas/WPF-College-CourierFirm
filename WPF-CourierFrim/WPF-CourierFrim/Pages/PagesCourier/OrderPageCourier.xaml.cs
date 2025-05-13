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
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls;
using WPF_CourierFrim.UserControls.CardsAdmin;
using WPF_CourierFrim.UserControls.CardsCourier;
using static WPF_CourierFrim.UserControls.CardsCourier.CardOrderCourier;

namespace WPF_CourierFrim.Pages.PagesCourier
{
    /// <summary>
    /// Логика взаимодействия для OrderPageCourier.xaml
    /// </summary>
    public partial class OrderPageCourier : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private OrderDataService _orderDataService;
        private Employee _thisCourier;

        // Конструктор
        public OrderPageCourier(Employee courier)
        {
            InitializeComponent();

            _dbContext = new();
            _thisCourier = courier;
            _orderDataService = new(filterCB, sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateIC);
            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var orders = _dbContext.Orders.ToList();

            orders = _orderDataService.ApplyCourier(orders, _thisCourier);
            orders = _orderDataService.ApplyFilter(orders);
            orders = _orderDataService.ApplySort(orders);
            orders = _orderDataService.ApplySearch(orders);

            cardsIC.Items.Clear();
            foreach (var order in orders)
            {
                var card = new CardOrderCourier(order, _thisCourier);

                if (order.DatetimeCompletion != null)
                {
                    card.Opacity = 0.5;
                }
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void AcceptOrderRequested(object sender, OrderEventArgs e) => UpdateIC();

    }
}
