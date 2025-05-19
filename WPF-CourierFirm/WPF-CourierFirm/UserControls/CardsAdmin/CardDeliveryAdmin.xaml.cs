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
using WPF_CourierFrim.WindowsDialog;
using WPF_CourierFrim.WindowsInfo;

namespace WPF_CourierFrim.UserControls.CardsAdmin
{
    /// <summary>
    /// Логика взаимодействия для CardDeliveryAdmin.xaml
    /// </summary>
    public partial class CardDeliveryAdmin : UserControl
    {
        // Поля 
        public Delivery Delivery { get; private set; }
        private CourierServiceContext _dbContext;
        private Delivery _delivery;

        // Конструктор
        public CardDeliveryAdmin(Delivery delivery)
        {
            InitializeComponent();

            _delivery = delivery;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_delivery);
            DataContext = _delivery;
        }

        // Обработчики событий
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindowDelivery infoWindowDelivery = new(_delivery);
            ComponentsHelper.ShowDialogWindowDark(infoWindowDelivery);
        }

        // События
        public event EventHandler<DeliveryEventArgs> ChangeStatusRequested;
        public class DeliveryEventArgs : EventArgs
        {
            public Delivery Delivery { get; set; }
        }
    }
}