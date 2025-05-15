using Dadata.Model;
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
using Dadata;
using System.Collections.ObjectModel;

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTransportWindow.xaml
    /// </summary>
    public partial class AddTransportWindow : Window
    {
        // Поля и свойства
        private Employee _courier;

        // Конструктор
        public AddTransportWindow(Employee courier)
        {
            InitializeComponent();
            _courier = courier;

            if (_courier.Transport != null)
            {
                // Заполняем поля данными для редактирования
                licensePlateTB.Text = _courier.Transport.LicensePlate;
                brandTB.Text = _courier.Transport.Brand;
                modelTB.Text = _courier.Transport.Model;
                colorTB.Text = _courier.Transport.Color;
                yearTB.Text = _courier.Transport.Year.ToString();
            }
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            /*string name = nameTB.Text;
            string email = emailTB.Text;
            string phone = phoneTB.PhoneNumber;
            string address = addressTB.Text;

            bool notError = Limitators.OrgLimitator(null, name, email, phone, address);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;*/

            //OrganisationService.AddOgranisation(name, email, phone, address);
            Close();
        }
    }
}
