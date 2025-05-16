using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPF_CourierFrim.Windows.DialogWindows;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для StatisticPageAdmin.xaml
    /// </summary>
    public partial class StatisticPageAdmin : Page, INotifyPropertyChanged
    {
        // Свойства для диаграммы по доставкам
        public SeriesCollection Series { get; set; }
        public string[] Labels { get; set; }
        public Axis AxisY { get; set; }

        // Поля
        private Employee _admin;

        // Конструктор
        public StatisticPageAdmin(Employee admin)  
        {
            InitializeComponent();

            _admin = admin;
            DeliveriesDiagramLoad(DateTime.Now.Year); // настоящий год
            DataContext = this;

            filterCB.ItemsSource = new[] { "2025 год", "2024 год", "2023 год" };
            filterCB.SelectedIndex = 0;
        }

        // Методы
        private void DeliveriesDiagramLoad(int year) // Метод для отображенмия данных по доставкам
        {
            // Оглавление месяцов
            Labels = new[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            // Данные: проценты по месяцам
            var statistics = DeliveryService.GetMonthlyCompletedDeliveries(year);

            // Преобразование данных для графика
            var deliveries = new List<double>();
            foreach (var month in Labels)
            {
                var stat = statistics.Find(s => s.MonthYear.StartsWith(month.ToLower()));
                deliveries.Add(stat?.CompletedDeliveries ?? 0.1);
            }

            // Автоматический расчет максимального значения с запасом 10%
            var maxValue = deliveries.Max() * 1.1;

            // Настройка оси Y
            AxisY = new Axis
            {
                MinValue = 0,  
                MaxValue = maxValue,
                LabelFormatter = value => value.ToString("N0")
            };

            // Настройка столбцов
            Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Доставок",
                    Values = new ChartValues<double>(deliveries),
                    Fill = new SolidColorBrush(Color.FromRgb(122, 146, 194)), // Цвет столбцов
                    Foreground = System.Windows.Media.Brushes.White,
                    DataLabels = true,                              // Показывать значения
                    LabelPoint = point => $"{point.Y.ToString("N0")}"              // Формат подписей
                }
            };

            // Обновление привязок
            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(AxisY));
        }

        private void filterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterCB.SelectedItem != null)
            {
                var year = int.Parse(filterCB.SelectedItem.ToString().Substring(0, 4));
                DeliveriesDiagramLoad(year);
            }
        }

        // Реализация интерфейса
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Обработчики событий
        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            GeneratedReportWindow window = new(_admin);
            ComponentsHelper.ShowDialogWindowDark(window);
        }
    }
}
