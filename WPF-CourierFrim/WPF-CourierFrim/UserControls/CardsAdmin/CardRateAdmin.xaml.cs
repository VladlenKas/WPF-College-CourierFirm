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
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Windows.WindowsDialog;
using WPF_CourierFrim.Windows.WindowsInfo;

namespace WPF_CourierFrim.UserControls.CardsAdmin
{
    /// <summary>
    /// Логика взаимодействия для CardRateAdmin.xaml
    /// </summary>
    public partial class CardRateAdmin : UserControl
    {
        // Поля 
        public Rate Rate { get; private set; }
        private CourierServiceContext _dbContext;
        private Rate _rate;

        // Конструктор
        public CardRateAdmin(Rate rate)
        {
            InitializeComponent();

            _rate = rate;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_rate);
            DataContext = _rate;
        }

        // Обработчики событий
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool delete = MessageHelper.ConfirmDelete();
            if (!delete) return;

            _dbContext = new();
            RateService.DeleteRate(_rate);

            RateRequested?.Invoke(this, new RateEventArgs { Rate = this.Rate }); // Уведомляем род. страницу об удалении
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditRateWindow window = new(_rate);
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (!saved) return;

            _dbContext = new();
            RateRequested?.Invoke(this, new RateEventArgs { Rate = this.Rate }); // Уведомляем род. страницу об удалении
        }

        // События
        public event EventHandler<RateEventArgs> RateRequested;
        public class RateEventArgs : EventArgs
        {
            public Rate Rate { get; set; }
        }
    }
}