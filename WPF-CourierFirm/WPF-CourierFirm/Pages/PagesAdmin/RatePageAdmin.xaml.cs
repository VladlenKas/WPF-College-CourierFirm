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
using static WPF_CourierFirm.UserControls.CardsAdmin.CardRateAdmin;

namespace WPF_CourierFirm.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для RatePageAdmin.xaml
    /// </summary>
    public partial class RatePageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private RateDataService _rateDataService;

        // Конструктор
        public RatePageAdmin()
        {
            InitializeComponent();

            _dbContext = new();
            _rateDataService = new(sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateIC);
            UpdateIC();

        }

        // Методы
        private void UpdateIC()
        {
            _dbContext = new();
            var rates = _dbContext.Rates.ToList();

            rates = _rateDataService.ApplySort(rates);
            rates = _rateDataService.ApplySearch(rates);

            cardsIC.Items.Clear();
            foreach (var rate in rates)
            {
                var card = new CardRateAdmin(rate);
                card.RateRequested += RateRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void RateRequested(object sender, RateEventArgs e) => UpdateIC();

        private void AddRate_Click(object sender, RoutedEventArgs e)
        {
            AddRateWindow window = new();
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateIC();
        }
    }
}
