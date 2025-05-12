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
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Windows.DialogWindows;
using WPF_CourierFrim.Classes.Services;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для OrderPageAdmin.xaml
    /// </summary>
    public partial class OrderPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private OrderDataService _orderDataService;

        // Конструктор
        public OrderPageAdmin()
        {
            InitializeComponent();
            _dbContext = new();

            filterCB.ItemsSource = new[] { "Все заказы", "Активные заказы", "Завершенные заказы" };
            filterCB.SelectedIndex = 1;
            sorterCB.ItemsSource = new[] { "По номеру заказа", "По организациям", "По тарифам (ценам)", "По ФИО клиента", "По весу заказа" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = true;

            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            _orderDataService = new(filterCB, sorterCB, searchTB, ascendingCHB);
            var orders = _dbContext.Orders.ToList();

            orders = _orderDataService.ApplyFilter(orders);
            orders = _orderDataService.ApplySort(orders);
            orders = _orderDataService.ApplySearch(orders);

            cardsIC.Items.Clear();
            foreach (var order in orders)
            {
                var card = new CardOrderAdmin(order);

                card.DeleteOrderRequested += DeleteOrderRequested;
                if (order.DatetimeCompletion != null)
                {
                    card.Opacity = 0.5;
                }
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void DeleteOrderRequested(object sender, OrderEventArgs e) => UpdateIC();


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow window = new();
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateIC();
        }


        private void AscendingCHB_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }

        private void SorterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }
    }
}
