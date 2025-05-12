using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Services
{
    // Заказы (поиск, сортировка, фильтрация)
    public class OrderDataService(ComboBox filterCB, ComboBox sorterCB, TextBox searchTB, CheckBox ascendingCHB)
    {
        private ComboBox _filterCB = filterCB;
        private ComboBox _sorterCB = sorterCB;
        private TextBox _searchTB = searchTB;
        private CheckBox _ascendingCHB = ascendingCHB;

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

            if (!ascending)
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
    }
}


