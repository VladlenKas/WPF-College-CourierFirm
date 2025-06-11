using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using WPF_CourierFirm.Classes;
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CourierFirm.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private CourierServiceContext dbContext;

        // Конструктор
        public AddOrderWindow()
        {
            InitializeComponent();
            dbContext = new();

            organisationCB.ItemsSource = dbContext.Organisations.ToList();
            rateCB.ItemsSource = dbContext.Rates.ToList();
            typeContentCB.ItemsSource = dbContext.ContentTypes.ToList();
        }

        // Методы
        private void AddOrder(Order? order, Organisation? organisation, Rate? rate,
            string receivingAddress, string deliveryAddress, ContentType? contentType, string phoneClient,
            string fullnameClient, string content, decimal weight)
        {
            bool notError = Limitators.OrderLimitator(null, organisation, rate, receivingAddress, deliveryAddress,
               contentType, phoneClient, fullnameClient, content, weight);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;
            OrderService.CreateOrder(organisation, rate, receivingAddress, deliveryAddress,
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

            AddOrder(null, organisation, rate, receivingAddress, deliveryAddress,
                contentType, phoneClient, fullnameClient, content, weight);
        }
    }
}
