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
using WPF_CourierFrim.UserControls.CardsCourier;
using static WPF_CourierFrim.UserControls.CardsCourier.CardDeliveryCourier;

namespace WPF_CourierFrim.Pages.PagesCourier
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPageCourier.xaml
    /// </summary>
    public partial class DeliveryPageCourier : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public DeliveryPageCourier(Employee employee)
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
            var deliveries = _dbContext.Deliveries
                .Where(r => r.EmployeeDeliveries
                    .Any(f => f.EmployeeId == _thisEmpoyee.EmployeeId))
                .ToList();

            // Фильтрация и сортировка

            cardsIC.Items.Clear();
            foreach (var delivery in deliveries)
            {
                var card = new CardDeliveryCourier(delivery);
                card.ChangeStatusRequested += ChangeStatusRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void ChangeStatusRequested(object sender, DeliveryEventArgs e) => UpdateIC();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
