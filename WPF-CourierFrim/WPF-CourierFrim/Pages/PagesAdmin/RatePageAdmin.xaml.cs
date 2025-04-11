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
using WPF_CourierFrim.Model;
using WPF_CourierFrim.UserControls.CardsAdmin;
using static WPF_CourierFrim.UserControls.CardsAdmin.CardRateAdmin;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для RatePageAdmin.xaml
    /// </summary>
    public partial class RatePageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;

        // Конструктор
        public RatePageAdmin()
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
            var rates = _dbContext.Rates.ToList();

            // Фильтрация и сортировка 

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
