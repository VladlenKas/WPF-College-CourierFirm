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
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Windows.WindowsInfo;

namespace WPF_CourierFrim.UserControls.CardsCourier
{
    /// <summary>
    /// Логика взаимодействия для CardDeliveryCourier.xaml
    /// </summary>
    public partial class CardDeliveryCourier : UserControl
    {        
        // Поля 
        public Delivery Delivery { get; private set; }
        private CourierServiceContext _dbContext;
        private Delivery _delivery;
        private Employee _employee;

        // Конструктор
        public CardDeliveryCourier(Delivery delivery, Employee employee)
        {
            InitializeComponent();

            _delivery = delivery;
            _employee = employee;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_delivery);

            ComponentsHelper.ToggleVisibilityButtonsDelivery(_delivery, getOrderBTN, handingOrderBTN,
                completeDeliveryBTN, cancellationDeliveryBTN); // смена видимости кнопок

            DataContext = _delivery;
        }

        // Обработчики событий
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowDelivery infoWindowDelivery = new(_delivery);
            ComponentsHelper.DarkenWindow(infoWindowDelivery);
        }

        private void GetOrder_Click(object sender, RoutedEventArgs e)
        {
            bool accept = MessageHelper.ConfirmChangeStatus();
            if (!accept) return;

            _dbContext = new();
            DeliveryService.GetOrder(_delivery);

            ChangeStatusRequested?.Invoke(this, new DeliveryEventArgs { Delivery = this.Delivery }); // Уведомляем род. страницу об удалении
        }

        private void HandingOrder_Click(object sender, RoutedEventArgs e)
        {
            bool accept = MessageHelper.ConfirmChangeStatus();
            if (!accept) return;

            _dbContext = new();
            DeliveryService.HandingOrder(_delivery);

            ChangeStatusRequested?.Invoke(this, new DeliveryEventArgs { Delivery = this.Delivery }); // Уведомляем род. страницу об удалении
        }

        private void CancellationDelivery_Click(object sender, RoutedEventArgs e)
        {
            bool accept = MessageHelper.ConfirmCancellationOrder();
            if (!accept) return;

            _dbContext = new();
            DeliveryService.CancellationDelivery(_delivery);

            ChangeStatusRequested?.Invoke(this, new DeliveryEventArgs { Delivery = this.Delivery }); // Уведомляем род. страницу об удалении
        }

        // События
        public event EventHandler<DeliveryEventArgs> ChangeStatusRequested;
        public class DeliveryEventArgs : EventArgs
        {
            public Delivery Delivery { get; set; }
        }
    }
}