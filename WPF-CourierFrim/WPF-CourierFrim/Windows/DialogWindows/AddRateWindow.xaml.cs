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
using System.Xml.Linq;

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddRateWindow.xaml
    /// </summary>
    public partial class AddRateWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }

        // Конструктор
        public AddRateWindow()
        {
            InitializeComponent();
        }

        // Методы
        private void AddRate(string name, int cost, string description)
        {
            bool notError = Limitators.RateLimitator(null, name, cost, description);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;

            RateService.AddRate(name, cost, description);
            Saved = true;
            Close();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            int cost = TypeHelper.IntParse(costTB.Text);
            string description = descriptionTB.Text;

            AddRate(name, cost, description);
        }
    }
}
