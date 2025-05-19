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
using System.Collections.ObjectModel;
using System.Net;
using System.Xml.Linq;

namespace WPF_CourierFrim.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTransportWindow.xaml
    /// </summary>
    public partial class AddTransportWindow : Window
    {
        // Поля и свойства
        private Employee _courier;
        public bool AddedTransport { get; set; }

        // Конструктор
        public AddTransportWindow(Employee courier)
        {
            InitializeComponent();
            _courier = courier;

            if (_courier.Transport != null)
            {
                // Заполняем поля данными для редактирования,
                // если у курьера уже имеется ТС
                licensePlateTB.Text = _courier.Transport.LicensePlate;
                brandTB.Text = _courier.Transport.Brand;
                modelTB.Text = _courier.Transport.Model;
                colorTB.Text = _courier.Transport.Color;
                yearTB.Text = _courier.Transport.Year.ToString();
            }
        }

        // Методы
        private void AddTransport(string licensePlate, string brand,
            string model, string color, short year)
        {
            bool notError = Limitators.TransportLimitator(null, licensePlate, brand, model, color, year);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;

            TransportService.AddTransport(_courier, licensePlate, brand, model, color, year);
            AddedTransport = true;
            Close();
        }

        private void EditTransport(Transport transport, string licensePlate, string brand,
            string model, string color, short year)
        {
            bool notError = Limitators.TransportLimitator(_courier.Transport, licensePlate, brand, model, color, year);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmEdit();
            if (!accept) return;

            TransportService.EditTransport(transport, licensePlate, brand, model, color, year);
            Close();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string licensePlate = licensePlateTB.Text;
            string brand = brandTB.Text;
            string model = modelTB.Text;
            string color = colorTB.Text;
            short year = TypeHelper.ShortParse(yearTB.Text);

            if (_courier.Transport != null)
            {
                EditTransport(_courier.Transport, licensePlate, brand, model, color, year);
            }
            else
            {
                AddTransport(licensePlate, brand, model, color, year);
            }
        }
    }
}
