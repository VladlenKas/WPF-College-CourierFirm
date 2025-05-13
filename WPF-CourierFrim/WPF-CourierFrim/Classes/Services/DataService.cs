using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Services
{
    // Заказы (поиск, сортировка, фильтрация)
    public class OrderDataService
    {
        private ComboBox _filterCB;
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateIC;

        public OrderDataService(ComboBox filterCB, ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _filterCB = filterCB;
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateIC = Action;

            filterCB.ItemsSource = new[] { "Все заказы", "Активные заказы", "Завершенные заказы" };
            filterCB.SelectedIndex = 1;
            sorterCB.ItemsSource = new[] { "По номеру заказа", "По организациям", "По тарифам (ценам)", "По ФИО клиента", "По весу заказа" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = false;

            OnTriggers();
        }

        // Поиск
        public List<Order> ApplySearch(List<Order> orders)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                orders = orders.Where(r => 
                    r.OrderId.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Organisation.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Rate.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.FullnameClient.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.PhoneClient.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
            }
            return orders;
        }

        // Сортировка
        public List<Order> ApplySort(List<Order> orders)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                switch (sortIndex)
                {
                    case 1:
                        return orders.OrderBy(e => e.Organisation.Name).ToList();
                    case 2:
                        return orders.OrderBy(e => e.Rate.Cost).ToList();
                    case 3:
                        return orders.OrderBy(e => e.FullnameClient).ToList();
                    case 4:
                        return orders.OrderBy(e => e.DatetimeCreation).ToList();
                    case 5:
                        return orders.OrderBy(e => e.Content.Weight).ToList();
                    default:
                        return orders.OrderBy(e => e.OrderId).ToList();
                }
            }
            else
            {
                switch (sortIndex)
                {
                    case 1:
                        return orders.OrderByDescending(e => e.Organisation.Name).ToList();
                    case 2:
                        return orders.OrderByDescending(e => e.Rate.Cost).ToList();
                    case 3:
                        return orders.OrderByDescending(e => e.FullnameClient).ToList();
                    case 4:
                        return orders.OrderByDescending(e => e.DatetimeCreation).ToList();
                    case 5:
                        return orders.OrderByDescending(e => e.Content.Weight).ToList();
                    default:
                        return orders.OrderByDescending(e => e.OrderId).ToList();
                }
            }
        }

        // Фильтрация
        public List<Order> ApplyFilter(List<Order> orders)
        {
            int dateIndex = _filterCB.SelectedIndex;

            switch (dateIndex)
            {
                case 1:
                    return orders.Where(r => r.DatetimeCompletion == null).ToList(); // Активные
                case 2:
                    return orders.Where(r => r.DatetimeCompletion != null).ToList(); // Завершенные 
                default:
                    return orders.ToList(); // Все
            }
        }

        // Фильтра по курьеру
        public List<Order> ApplyCourier(List<Order> orders, Employee courier)
        {
            // Возвращает только те заказы, которые еще не имеют назначенного курьера
            // ИЛИ имеют доставку с конкретным курьером
            return orders
                .Where(order =>
                    order.DatetimeCompletion == null ||
                    order.Deliveries
                        .SelectMany(d => d.EmployeeDeliveries)
                        .Any(ed => ed.EmployeeId == courier.EmployeeId)
                )
                .ToList();
        }

        // Свойства
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

        private void ResetFiltersBTN_Click(object sender, RoutedEventArgs e)
        {
            OffTriggers();

            _filterCB.SelectedIndex = 1;
            _sorterCB.SelectedIndex = 0;
            _ascendingCHB.IsChecked = false;
            _searchTB.Clear();

            UpdateIC();
            OnTriggers();
        }


        // Включает тригеры
        private void OnTriggers()
        {
            _filterCB.SelectionChanged += FilterCB_SelectionChanged;
            _sorterCB.SelectionChanged += SorterCB_SelectionChanged;
            _ascendingCHB.Click += AscendingCHB_Click;
            _searchBTN.Click += SearchBTN_Click;
            _reserFiltersBTN.Click += ResetFiltersBTN_Click;
        }

        // Выключает тригеры
        private void OffTriggers()
        {
            _filterCB.SelectionChanged -= FilterCB_SelectionChanged;
            _sorterCB.SelectionChanged -= SorterCB_SelectionChanged;
            _ascendingCHB.Click -= AscendingCHB_Click;
            _searchBTN.Click -= SearchBTN_Click;
            _reserFiltersBTN.Click -= ResetFiltersBTN_Click;
        }
    }
}


