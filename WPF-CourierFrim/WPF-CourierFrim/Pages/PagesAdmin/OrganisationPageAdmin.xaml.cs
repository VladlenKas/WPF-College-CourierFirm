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
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls.CardsAdmin;
using WPF_CourierFrim.Windows.DialogWindows;
using static WPF_CourierFrim.UserControls.CardsAdmin.CardOrganisationAdmin;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для OrganisationPageAdmin.xaml
    /// </summary>
    public partial class OrganisationPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;

        // Конструктор
        public OrganisationPageAdmin()
        {
            InitializeComponent();
            _dbContext = new();

            // Загрузка комбобоксов и тд

            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var organisations = _dbContext.Organisations.ToList();

            // Фильтрация и сортировка 

            cardsIC.Items.Clear();
            foreach (var org in organisations)
            {
                var card = new CardOrganisationAdmin(org);
                card.OrgRequested += OrgRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void OrgRequested(object sender, OrgEventArgs e) => UpdateIC();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddOrg_Click(object sender, RoutedEventArgs e)
        {
            AddOrgWindow window = new();
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateIC();
        }
    }
}
