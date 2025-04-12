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

namespace WPF_CourierFrim.Windows.DialogWindows
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

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            string email = emailTB.Text;
            string phone = phoneTB.PhoneNumber;
            string address = addressTB.Text;

            bool notError = Limitators.OrgLimitator(null, name, email, phone, address);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmEdit();
            if (!accept) return;

            OrganisationService.AddOgranisation(name, email, phone, address);
            Saved = true;
            Close();
        }
    }
}
