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
using System.Windows.Shapes;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Windows.Media.Media3D;

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private CourierServiceContext dbContext;
        private Order _order;

        // Конструктор
        public EditOrderWindow(Order order)
        {
            InitializeComponent();
            dbContext = new();
            _order = order;
            DataContext = _order;

            organisationCB.ItemsSource = dbContext.Organisations.ToList();
            rateCB.ItemsSource = dbContext.Rates.ToList();
            typeContentCB.ItemsSource = dbContext.ContentTypes.ToList();

            organisationCB.SelectedItem = dbContext.Organisations.Single(r => r.OrganisationId == _order.Organisation.OrganisationId);
            rateCB.SelectedItem = dbContext.Rates.Single(r => r.RateId == _order.RateId);
            typeContentCB.SelectedItem = dbContext.ContentTypes.Single(r => r.ContentTypeId == _order.Content.ContentTypeId);
            receivingAddressTB.Text = _order.ReceivingAddress;
            deliveryAddressTB.Text = _order.DeliveryAddress;
            fullnameClientTB.Text = _order.FullnameClient;
            phoneClientTB.PhoneNumber = _order.PhoneClient;
            contentTB.Text = _order.Content.Description;
            weightTB.Text = _order.Content.Weight.ToString();
        }

        // Методы
        private void EditOrder(Order? order, Organisation? organisation, Rate? rate,
            string receivingAddress, string deliveryAddress, ContentType? contentType, string phoneClient,
            string fullnameClient, string content, decimal weight)
        {
            bool notError = Limitators.OrderLimitator(_order, organisation, rate, receivingAddress, deliveryAddress,
                contentType, phoneClient, fullnameClient, content, weight);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmEdit();
            if (!accept) return;
            OrderService.EditOrder(_order, organisation, rate, receivingAddress, deliveryAddress,
                contentType, phoneClient, fullnameClient, content, weight);
            Saved = true;
            Close();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Organisation organisation = (Organisation?)organisationCB.SelectedItem;
            Rate rate = (Rate?)rateCB.SelectedItem;
            ContentType contentType = (ContentType?)typeContentCB.SelectedItem;
            string receivingAddress = receivingAddressTB.Text;
            string deliveryAddress = deliveryAddressTB.Text;
            string fullnameClient = fullnameClientTB.Text;
            string phoneClient = phoneClientTB.PhoneNumber;
            string content = contentTB.Text;
            decimal weight = TypeHelper.DecemalParse(weightTB.Text);

            EditOrder(_order, organisation, rate, receivingAddress, deliveryAddress,
                contentType, phoneClient, fullnameClient, content, weight);
        }
    }
}
