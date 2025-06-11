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
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.Model;
using WPF_CourierFirm.WindowsDialog;
using WPF_CourierFirm.WindowsInfo;

namespace WPF_CourierFirm.UserControls.CardsCourier
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

        // Конструктор
        public CardDeliveryCourier(Delivery delivery)
        {
            InitializeComponent();

            if (delivery.StatusDeliveryId == 3 || delivery.StatusDeliveryId == 4)
            {
                ButtonsSP.Visibility = Visibility.Visible;
            }
            else
            {
                infoBTN.Visibility = Visibility.Visible;
            }

            _delivery = delivery;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_delivery);

            ComponentsHelper.ToggleVisibilityButtonsDelivery(_delivery, getOrderBTN, 
                handingOrderBTN, cancellationDeliveryBTN); // смена видимости кнопок

            DataContext = _delivery;
        }

        // Обработчики событий
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowDelivery infoWindowDelivery = new(_delivery);
            ComponentsHelper.ShowDialogWindowDark(infoWindowDelivery);
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
            PaymentWindow paymentWindow = new(_delivery);
            ComponentsHelper.ShowDialogWindowDark(paymentWindow);

            bool saved = paymentWindow.Saved;
            if (!saved) return;

            _dbContext = new();
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