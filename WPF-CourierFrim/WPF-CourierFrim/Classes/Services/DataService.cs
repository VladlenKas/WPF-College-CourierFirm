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

        // СБрос фильтров
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

    // Доставки (поиск, сортировка, фильтрация)
    public class DeliveryDataService
    {
        private ComboBox _filterCB;
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateIC;

        public DeliveryDataService(ComboBox filterCB, ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _filterCB = filterCB;
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateIC = Action;

            filterCB.ItemsSource = new[] { "Все доставки", "Активные доставки", "Завершенные доставки" };
            filterCB.SelectedIndex = 1;
            sorterCB.ItemsSource = new[] { "По номеру доставки", "По организациям", "По тарифам (ценам)", "По ФИО клиента", "По весу заказа",
                "По ФИО курьера" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = false;

            OnTriggers();
        }

        // Поиск
        public List<Delivery> ApplySearch(List<Delivery> deliveries)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                deliveries = deliveries.Where(r =>
                    // Основные проверки
                    r.DeliveryId.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Order.Organisation.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Order.Rate.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Order.FullnameClient.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Order.PhoneClient.Contains(search, StringComparison.OrdinalIgnoreCase) ||

                    // Проверки по курьеру
                    r.EmployeeDeliveries?
                        .Where(ed => ed.Employee != null)
                        .Any(ed =>
                            ed.Employee.Fullname.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            ed.Employee.Fullname.Contains(search, StringComparison.OrdinalIgnoreCase))
                    == true)
                .ToList();
            }
            return deliveries;
        }

        // Сортировка
        public List<Delivery> ApplySort(List<Delivery> deliveries)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                switch (sortIndex)
                {
                    case 1:
                        return deliveries.OrderBy(e => e.Order.Organisation.Name).ToList();
                    case 2:
                        return deliveries.OrderBy(e => e.Order.Rate.Cost).ToList();
                    case 3:
                        return deliveries.OrderBy(e => e.Order.FullnameClient).ToList();
                    case 4:
                        return deliveries.OrderBy(e => e.Order.DatetimeCreation).ToList();
                    case 5:
                        return deliveries.OrderBy(e => e.Order.Content.Weight).ToList();
                    case 6:
                        return deliveries.OrderBy(e => e.EmployeeDeliveries.Single(ed => ed.DeliveryId == e.DeliveryId)
                        .Employee.Fullname).ToList();
                    default:
                        return deliveries.OrderBy(e => e.DeliveryId).ToList();
                }
            }
            else
            {
                switch (sortIndex)
                {
                    case 1:
                        return deliveries.OrderByDescending(e => e.Order.Organisation.Name).ToList();
                    case 2:
                        return deliveries.OrderByDescending(e => e.Order.Rate.Cost).ToList();
                    case 3:
                        return deliveries.OrderByDescending(e => e.Order.FullnameClient).ToList();
                    case 4:
                        return deliveries.OrderByDescending(e => e.Order.DatetimeCreation).ToList();
                    case 5:
                        return deliveries.OrderByDescending(e => e.Order.Content.Weight).ToList();
                    case 6:
                        return deliveries.OrderByDescending(e => e.EmployeeDeliveries.Single(ed => ed.DeliveryId == e.DeliveryId)
                        .Employee.Fullname).ToList();
                    default:
                        return deliveries.OrderByDescending(e => e.DeliveryId).ToList();
                }
            }
        }

        // Фильтрация
        public List<Delivery> ApplyFilter(List<Delivery> deliveries)
        {
            int dateIndex = _filterCB.SelectedIndex;

            switch (dateIndex)
            {
                case 1:
                    return deliveries.Where(r => 
                        r.StatusDeliveryId == 3 ||
                        r.StatusDeliveryId == 4).ToList(); // Активные
                case 2:
                    return deliveries.Where(r =>
                        r.StatusDeliveryId == 1 ||
                        r.StatusDeliveryId == 2 ||
                        r.StatusDeliveryId == 5).ToList(); // Завершенные 
                default:
                    return deliveries.ToList(); // Все
            }
        }

        // Фильтр по курьеру
        public List<Delivery> ApplyCourier(List<Delivery> deliveries, Employee courier)
        {
            // Возвращает только те заказы, которые еще не имеют назначенного курьера
            // ИЛИ имеют доставку с конкретным курьером
            return deliveries
                .Where(delivery =>
                    delivery.DatetimePresentation == null ||
                    delivery.EmployeeDeliveries
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

        // Сброс фильтров
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


