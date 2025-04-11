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
using WPF_CourierFrim.UserControls.CardsCourier;
using static WPF_CourierFrim.UserControls.CardsAdmin.CardDeliveryAdmin;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPageAdmin.xaml
    /// </summary>
    public partial class DeliveryPageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public DeliveryPageAdmin()
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
            var deliveries = _dbContext.Deliveries.ToList();

            // Фильтрация и сортировка

            cardsIC.Items.Clear();
            foreach (var delivery in deliveries)
            {
                var card = new CardDeliveryAdmin(delivery);
                card.ChangeStatusRequested += ChangeStatusRequested;
                cardsIC.Items.Add(card);
            }
        }

        // Обработчики событий
        private void ChangeStatusRequested(object sender, DeliveryEventArgs e) => UpdateIC();

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
