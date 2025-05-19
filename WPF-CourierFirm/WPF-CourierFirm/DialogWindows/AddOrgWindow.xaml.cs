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
using System.Net;
using System.Xml.Linq;

namespace WPF_CourierFrim.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddOrgWindow.xaml
    /// </summary>
    public partial class AddOrgWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }

        // Конструктор
        public AddOrgWindow()
        {
            InitializeComponent();
        }

        // Методы
        private void AddOrganisation(string name, string email, string phone, string address)
        {
            bool notError = Limitators.OrgLimitator(null, name, email, phone, address);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;

            OrganisationService.AddOgranisation(name, email, phone, address);
            Saved = true;
            Close();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            string email = emailTB.Text;
            string phone = phoneTB.PhoneNumber;
            string address = addressTB.Text;

            AddOrganisation(name, email, phone, address);
        }
    }
}
