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
    // Заказы (поиск, сортировка, фильтрация, +курьер)
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
            sorterCB.ItemsSource = new[] { "По номеру заказа", "По организациям", "По тарифам (ценам)", 
                "По ФИО клиента", "По весу заказа" };
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
                return sortIndex switch
                {
                    1 => orders.OrderBy(e => e.Organisation.Name).ToList(),
                    2 => orders.OrderBy(e => e.Rate.Cost).ToList(),
                    3 => orders.OrderBy(e => e.FullnameClient).ToList(),
                    4 => orders.OrderBy(e => e.DatetimeCreation).ToList(),
                    5 => orders.OrderBy(e => e.Content.Weight).ToList(),
                    _ => orders.OrderBy(e => e.OrderId).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => orders.OrderByDescending(e => e.Organisation.Name).ToList(),
                    2 => orders.OrderByDescending(e => e.Rate.Cost).ToList(),
                    3 => orders.OrderByDescending(e => e.FullnameClient).ToList(),
                    4 => orders.OrderByDescending(e => e.DatetimeCreation).ToList(),
                    5 => orders.OrderByDescending(e => e.Content.Weight).ToList(),
                    _ => orders.OrderByDescending(e => e.OrderId).ToList(),
                };
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

        // Фильтр по курьеру
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

    // Доставки (поиск, сортировка, фильтрация, +курьер)
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
            sorterCB.ItemsSource = new[] { "По номеру доставки", "По организациям", "По тарифам (ценам)", 
                "По ФИО клиента", "По весу заказа", "По ФИО курьера" };
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
                return sortIndex switch
                {
                    1 => deliveries.OrderBy(e => e.Order.Organisation.Name).ToList(),
                    2 => deliveries.OrderBy(e => e.Order.Rate.Cost).ToList(),
                    3 => deliveries.OrderBy(e => e.Order.FullnameClient).ToList(),
                    4 => deliveries.OrderBy(e => e.Order.DatetimeCreation).ToList(),
                    5 => deliveries.OrderBy(e => e.Order.Content.Weight).ToList(),
                    6 => deliveries.OrderBy(e => e.EmployeeDeliveries
                                            .Single(ed => ed.DeliveryId == e.DeliveryId).Employee.Fullname).ToList(),
                    _ => deliveries.OrderBy(e => e.DeliveryId).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => deliveries.OrderByDescending(e => e.Order.Organisation.Name).ToList(),
                    2 => deliveries.OrderByDescending(e => e.Order.Rate.Cost).ToList(),
                    3 => deliveries.OrderByDescending(e => e.Order.FullnameClient).ToList(),
                    4 => deliveries.OrderByDescending(e => e.Order.DatetimeCreation).ToList(),
                    5 => deliveries.OrderByDescending(e => e.Order.Content.Weight).ToList(),
                    6 => deliveries.OrderByDescending(e => e.EmployeeDeliveries
                                            .Single(ed => ed.DeliveryId == e.DeliveryId).Employee.Fullname).ToList(),
                    _ => deliveries.OrderByDescending(e => e.DeliveryId).ToList(),
                };
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
            // Возвращает доставки только с конкретным курьером
            return deliveries
                .Where(d => d.EmployeeDeliveries?
                    .Any(ed => ed.EmployeeId == courier.EmployeeId) == true
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

    // Сотрудники (поиск, сортировка, фильтрация)
    public class EmployeeDataService
    {
        private ComboBox _filterCB;
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateDG;

        public EmployeeDataService(ComboBox filterCB, ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _filterCB = filterCB;
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateDG = Action;

            filterCB.ItemsSource = new[] { "Все должности", "Администраторы", "Курьеры" };
            filterCB.SelectedIndex = 0;
            sorterCB.ItemsSource = new[] { "По ФИО", "По возрасту", "По должности" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = true;

            OnTriggers();
        }

        // Поиск
        public List<Employee> ApplySearch(List<Employee> employees)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                employees = employees.Where(r =>
                    r.Fullname.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Birthday.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Passport.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Phone.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Login.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Password.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Transport?.InfoCar.Contains(search, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
            }
            return employees;
        }

        // Сортировка
        public List<Employee> ApplySort(List<Employee> employees)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                return sortIndex switch
                {
                    1 => employees.OrderBy(e => e.Birthday).ToList(),
                    2 => employees.OrderBy(e => e.Post.Name).ToList(),
                    _ => employees.OrderBy(e => e.Fullname).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => employees.OrderByDescending(e => e.Birthday).ToList(),
                    2 => employees.OrderByDescending(e => e.Post.Name).ToList(),
                    _ => employees.OrderByDescending(e => e.Fullname).ToList(),
                };
            }
        }

        // Фильтрация
        public List<Employee> ApplyFilter(List<Employee> employees)
        {
            int dateIndex = _filterCB.SelectedIndex;

            switch (dateIndex)
            {
                case 1:
                    return employees.Where(r => r.PostId == 1).ToList(); // Админы
                case 2:
                    return employees.Where(r => r.PostId == 2).ToList(); // Курьеры 
                default:
                    return employees.ToList(); // Все
            }
        }

        // Свойства
        private void AscendingCHB_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        private void SorterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        // СБрос фильтров
        private void ResetFiltersBTN_Click(object sender, RoutedEventArgs e)
        {
            OffTriggers();

            _filterCB.SelectedIndex = 0;
            _sorterCB.SelectedIndex = 0;
            _ascendingCHB.IsChecked = true;
            _searchTB.Clear();

            UpdateDG();
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

    // Автомобили (поиск, сортировка)
    public class TransportDataService
    {
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateDG;

        public TransportDataService(ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateDG = Action;

            sorterCB.ItemsSource = new[] { "По ФИО курьера", "По номеру (авто)", "По цвету", "По бренду",
                "По модели", "По году выпуска" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = false;

            OnTriggers();
        }

        // Поиск
        public List<Transport> ApplySearch(List<Transport> transports)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                transports = transports.Where(r =>
                    r.InfoCar.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Employees?.First().Fullname?.Contains(search, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            }
            return transports;
        }

        // Сортировка
        public List<Transport> ApplySort(List<Transport> transports)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                return sortIndex switch
                {
                    1 => transports.OrderBy(e => e.LicensePlate).ToList(),
                    2 => transports.OrderBy(e => e.Color).ToList(),
                    3 => transports.OrderBy(e => e.Brand).ToList(),
                    4 => transports.OrderBy(e => e.Model).ToList(),
                    5 => transports.OrderBy(e => e.Year).ToList(),
                    _ => transports.OrderBy(e => e.Employees.First().Fullname).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => transports.OrderByDescending(e => e.LicensePlate).ToList(),
                    2 => transports.OrderByDescending(e => e.Color).ToList(),
                    3 => transports.OrderByDescending(e => e.Brand).ToList(),
                    4 => transports.OrderByDescending(e => e.Model).ToList(),
                    5 => transports.OrderByDescending(e => e.Year).ToList(),
                    _ => transports.OrderByDescending(e => e.Employees.First().Fullname).ToList(),
                };
            }
        }

        // Свойства
        private void AscendingCHB_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        private void SorterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateDG();
        }

        // Сброс фильтров
        private void ResetFiltersBTN_Click(object sender, RoutedEventArgs e)
        {
            OffTriggers();

            _sorterCB.SelectedIndex = 0;
            _ascendingCHB.IsChecked = false;
            _searchTB.Clear();

            UpdateDG();
            OnTriggers();
        }


        // Включает тригеры
        private void OnTriggers()
        {
            _sorterCB.SelectionChanged += SorterCB_SelectionChanged;
            _ascendingCHB.Click += AscendingCHB_Click;
            _searchBTN.Click += SearchBTN_Click;
            _reserFiltersBTN.Click += ResetFiltersBTN_Click;
        }

        // Выключает тригеры
        private void OffTriggers()
        {
            _sorterCB.SelectionChanged -= SorterCB_SelectionChanged;
            _ascendingCHB.Click -= AscendingCHB_Click;
            _searchBTN.Click -= SearchBTN_Click;
            _reserFiltersBTN.Click -= ResetFiltersBTN_Click;
        }
    }

    // Организации (поиск, сортировка)
    public class OrganisationDataService
    {
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateIC;

        public OrganisationDataService(ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateIC = Action;

            sorterCB.ItemsSource = new[] { "По названию", "По адресу", "По эл. почте", "По телефону" };
            sorterCB.SelectedIndex = 0;
            ascendingCHB.IsChecked = true;

            OnTriggers();
        }

        // Поиск
        public List<Organisation> ApplySearch(List<Organisation> organisations)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                organisations = organisations.Where(r =>
                    // Основные проверки
                    r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Address.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Phone.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }
            return organisations;
        }

        // Сортировка
        public List<Organisation> ApplySort(List<Organisation> organisations)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                return sortIndex switch
                {
                    1 => organisations.OrderBy(e => e.Address).ToList(),
                    2 => organisations.OrderBy(e => e.Email).ToList(),
                    3 => organisations.OrderBy(e => e.Phone).ToList(),
                    _ => organisations.OrderBy(e => e.Name).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => organisations.OrderByDescending(e => e.Address).ToList(),
                    2 => organisations.OrderByDescending(e => e.Email).ToList(),
                    3 => organisations.OrderByDescending(e => e.Phone).ToList(),
                    _ => organisations.OrderByDescending(e => e.Name).ToList(),
                };
            }
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

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }

        // Сброс фильтров
        private void ResetFiltersBTN_Click(object sender, RoutedEventArgs e)
        {
            OffTriggers();

            _sorterCB.SelectedIndex = 0;
            _ascendingCHB.IsChecked = true;
            _searchTB.Clear();

            UpdateIC();
            OnTriggers();
        }


        // Включает тригеры
        private void OnTriggers()
        {
            _sorterCB.SelectionChanged += SorterCB_SelectionChanged;
            _ascendingCHB.Click += AscendingCHB_Click;
            _searchBTN.Click += SearchBTN_Click;
            _reserFiltersBTN.Click += ResetFiltersBTN_Click;
        }

        // Выключает тригеры
        private void OffTriggers()
        {
            _sorterCB.SelectionChanged -= SorterCB_SelectionChanged;
            _ascendingCHB.Click -= AscendingCHB_Click;
            _searchBTN.Click -= SearchBTN_Click;
            _reserFiltersBTN.Click -= ResetFiltersBTN_Click;
        }
    }

    // Тарифы (поиск, сортировка)
    public class RateDataService
    {
        private ComboBox _sorterCB;
        private TextBox _searchTB;
        private CheckBox _ascendingCHB;
        private Button _reserFiltersBTN;
        private Button _searchBTN;
        private Action UpdateIC;

        public RateDataService(ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB,
            Button searchBTN, Button reserFiltersBTN, Action Action)
        {
            _sorterCB = sorterCB;
            _searchTB = searchTB;
            _ascendingCHB = ascendingCHB;
            _searchBTN = searchBTN;
            _reserFiltersBTN = reserFiltersBTN;
            UpdateIC = Action;

            sorterCB.ItemsSource = new[] { "По названию", "По цене" };
            sorterCB.SelectedIndex = 1;
            ascendingCHB.IsChecked = true;

            OnTriggers();
        }

        // Поиск
        public List<Rate> ApplySearch(List<Rate> rates)
        {
            string search = _searchTB.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(search))
            {
                rates = rates.Where(r =>
                    // Основные проверки
                    r.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Cost.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }
            return rates;
        }

        // Сортировка
        public List<Rate> ApplySort(List<Rate> rates)
        {
            int sortIndex = _sorterCB.SelectedIndex;
            bool ascending = (bool)_ascendingCHB.IsChecked;

            if (ascending)
            {
                return sortIndex switch
                {
                    1 => rates.OrderBy(e => e.Cost).ToList(),
                    _ => rates.OrderBy(e => e.Name).ToList(),
                };
            }
            else
            {
                return sortIndex switch
                {
                    1 => rates.OrderByDescending(e => e.Cost).ToList(),
                    _ => rates.OrderByDescending(e => e.Name).ToList(),
                };
            }
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

        private void SearchBTN_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null) UpdateIC();
        }

        // Сброс фильтров
        private void ResetFiltersBTN_Click(object sender, RoutedEventArgs e)
        {
            OffTriggers();

            _sorterCB.SelectedIndex = 1;
            _ascendingCHB.IsChecked = true;
            _searchTB.Clear();

            UpdateIC();
            OnTriggers();
        }


        // Включает тригеры
        private void OnTriggers()
        {
            _sorterCB.SelectionChanged += SorterCB_SelectionChanged;
            _ascendingCHB.Click += AscendingCHB_Click;
            _searchBTN.Click += SearchBTN_Click;
            _reserFiltersBTN.Click += ResetFiltersBTN_Click;
        }

        // Выключает тригеры
        private void OffTriggers()
        {
            _sorterCB.SelectionChanged -= SorterCB_SelectionChanged;
            _ascendingCHB.Click -= AscendingCHB_Click;
            _searchBTN.Click -= SearchBTN_Click;
            _reserFiltersBTN.Click -= ResetFiltersBTN_Click;
        }
    }
}


