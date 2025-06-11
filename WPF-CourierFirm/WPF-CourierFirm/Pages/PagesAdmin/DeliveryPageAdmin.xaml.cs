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
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.Model;
using WPF_CourierFirm.UserControls.CardsAdmin;
using WPF_CourierFirm.UserControls.CardsCourier;
using static WPF_CourierFirm.UserControls.CardsAdmin.CardDeliveryAdmin;

namespace WPF_CourierFirm.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPageAdmin.xaml
    /// </summary>
    public partial class DeliveryPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private DeliveryDataService _deliveryDataService;

        // Конструктор
        public DeliveryPageAdmin()
        {
            InitializeComponent();

            _dbContext = new();
            _deliveryDataService = new(filterCB, sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateIC);
            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var deliveries = _dbContext.Deliveries.ToList();

            deliveries = _deliveryDataService.ApplyFilter(deliveries);
            deliveries = _deliveryDataService.ApplySort(deliveries);
            deliveries = _deliveryDataService.ApplySearch(deliveries);

            cardsIC.Items.Clear();
            foreach (var delivery in deliveries)
            {
                var card = new CardDeliveryAdmin(delivery);

                card.ChangeStatusRequested += ChangeStatusRequested;
                if (delivery.StatusDeliveryId == 1 ||
                    delivery.StatusDeliveryId == 2 ||
                    delivery.StatusDeliveryId == 5)
                {
                    card.Opacity = 0.5;
                }

                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void ChangeStatusRequested(object sender, DeliveryEventArgs e) => UpdateIC();
    }
}
