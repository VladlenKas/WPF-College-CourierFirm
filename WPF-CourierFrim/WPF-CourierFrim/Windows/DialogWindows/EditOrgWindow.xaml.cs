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
using System.Net;
using System.Xml.Linq;

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditOrgWindow.xaml
    /// </summary>
    public partial class EditOrgWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private Organisation _organisation;

        // Конструктор
        public EditOrgWindow(Organisation organisation)
        {
            InitializeComponent();
            _organisation = organisation;

            nameTB.Text = organisation.Name;
            emailTB.Text = organisation.Email;
            phoneTB.PhoneNumber = organisation.Phone;
            addressTB.Text = organisation.Address;
        }

        // Методы
        private void EditOrganisation(string name, string email, string phone, string address)
        {
            bool notError = Limitators.OrgLimitator(_organisation, name, email, phone, address);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;

            OrganisationService.EditOgranisation(_organisation, name, email, phone, address);
            Saved = true;
            Close();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            string email = emailTB.Text;
            string phone = phoneTB.PhoneNumber;
            string address = addressTB.Text;

            EditOrganisation(name, email, phone, address);
        }
    }
}
