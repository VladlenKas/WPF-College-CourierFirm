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
using static WPF_CourierFrim.UserControls.CardsAdmin.CardOrganisationAdmin;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls.CardsAdmin;
using WPF_CourierFrim.Classes.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для TransportPageAdmin.xaml
    /// </summary>
    public partial class TransportPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private TransportDataService _transportDataService;
        // Конструктор
        public TransportPageAdmin()
        {
            InitializeComponent();

            _dbContext = new();
            _transportDataService = new(sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateDG);
            UpdateDG();
        }

        // Методы
        private void UpdateDG()
        {
            _dbContext = new();
            var transports = _dbContext.Transports.ToList();

            transports = _transportDataService.ApplySort(transports);
            transports = _transportDataService.ApplySearch(transports);

            itemsDG.ItemsSource = transports;
        }
    }
}
