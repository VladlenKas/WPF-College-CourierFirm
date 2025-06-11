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
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.DialogWindows;
using WPF_CourierFirm.Model;
using WPF_CourierFirm.UserControls.CardsAdmin;
using WPF_CourierFirm.WindowsDialog;
using static WPF_CourierFirm.UserControls.CardsAdmin.CardOrganisationAdmin;

namespace WPF_CourierFirm.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для OrganisationPageAdmin.xaml
    /// </summary>
    public partial class OrganisationPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private OrganisationDataService _organisationDataService;

        // Конструктор
        public OrganisationPageAdmin()
        {
            InitializeComponent();

            _dbContext = new();
            _organisationDataService = new(sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateIC);
            UpdateIC();
        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var organisations = _dbContext.Organisations.ToList();

            organisations = _organisationDataService.ApplySort(organisations);
            organisations = _organisationDataService.ApplySearch(organisations);

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

        private void AddOrg_Click(object sender, RoutedEventArgs e)
        {
            AddOrgWindow window = new();
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateIC();
        }
    }
}
