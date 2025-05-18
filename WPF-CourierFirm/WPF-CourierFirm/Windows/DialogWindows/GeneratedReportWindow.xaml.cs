using ControlzEx.Standard;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
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
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для GeneratedReportWindow.xaml
    /// </summary>
    public partial class GeneratedReportWindow : Window
    {
        // Поля и свойства
        private Employee _admin;
        private string _filepath;

        public GeneratedReportWindow(Employee admin)
        {
            InitializeComponent();
            _admin = admin;
        }

        // Методы
        private List<ReportData> GetReportData(DateOnly dateReportStart, DateOnly dateReportEnd)
        {
            // Получаем список выполненных или отмененых доставок за период
            CourierServiceContext dbContext = new CourierServiceContext();
            List<Delivery> deliveries = dbContext.Deliveries
                .Where(r =>
                    r.DatetimeReceiving != null &&
                    r.DatetimePresentation != null &&
                    r.DatetimeReceiving.Value.Date > dateReportStart.ToDateTime(TimeOnly.MinValue) &&
                    r.DatetimePresentation.Value.Date <= dateReportEnd.ToDateTime(TimeOnly.MinValue) &&
                    (r.StatusDeliveryId == 2 || r.StatusDeliveryId == 5))
                .ToList();

            // Создаем список с данными для отчета
            List<ReportData> reportDatas = new List<ReportData>();

            // Добавляем элементы в список, преобразуя данные
            foreach (var delivery in deliveries)
            {
                ReportData reportData = new()
                {
                    NumberDelivery = delivery.DeliveryId.ToString(),
                    FullnameCourier = delivery.EmployeeDeliveries.First().Employee.Fullname,
                    FullnameClient = delivery.Order.FullnameClient,
                    OrganisationName = delivery.Order.Organisation.Name,
                    RateName = delivery.Order.Rate.Name,
                    ContentTypeName = delivery.Order.Content.ContentType.Name,
                };

                // Преобразуем данные для метода оплаты
                reportData.PaymentMethod = delivery.PaymentMethod switch
                {
                    0 => "Наличный расчет",
                    1 => "Оплата картой",
                    _ => "Заказ отменен"
                };

                reportDatas.Add(reportData);
            }

            return reportDatas;
        }

        private void SaveDocument(DateOnly dateReportStart, DateOnly dateReportEnd)
        {
            // Проверка на корректность даты
            bool notError = Limitators.ReporttLimitator(dateReportStart, dateReportEnd);
            if (!notError) return;

            // Проверка на наличие выбранного пути
            if (string.IsNullOrEmpty(_filepath))
            {
                MessageHelper.MessageNullFilepath();
                return;
            }

            bool accept = MessageHelper.ConfirmSaveDocument();
            if (!accept) return;

            // Формируем документ и сохраняем
            string imageFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "LogoCourierFirmBlack.png");
            List<ReportData> deliveries = GetReportData(dateReportStart, dateReportEnd);
            DocumentService.GenerateReport(_filepath, dateReportStart, dateReportEnd, deliveries, _admin.Fullname, imageFilepath);

            // Открываем файл (или нет)
            bool openedDocument = (bool)openedDocumentCB.IsChecked;
            if (openedDocument == true)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _filepath,
                    UseShellExecute = true // Используем оболочку Windows для открытия файла
                });
            }

            Close();
        }

        private void OpenSaveFileDialog()
        {
            // Проверка на корректность даты
            DateOnly dateReportStart = TypeHelper.DateOnlyParse(dateReportStartTB.DateText);
            DateOnly dateReportEnd = TypeHelper.DateOnlyParse(dateReportEndTB.DateText);

            bool notError = Limitators.ReporttLimitator(dateReportStart, dateReportEnd);
            if (!notError) return;

            // Выбор пути (вызов проводника)
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Pdf Files|*.pdf*",
                Title = "Сохранить PDF документ",
                FileName = $"Отчет о доставках с {dateReportStart:dd.MM.yyyy} по {dateReportEnd:dd.MM.yyyy}"
            };

            // Если пользователь выбрал путь для сохранения чека
            if (saveFileDialog.ShowDialog() == true)
            {
                _filepath = $"{saveFileDialog.FileName}.pdf"; // Путь для открытия файла
                filepathTB.Text += _filepath;
            }
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void SaveDocument_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на выбранные даты и путь 
            DateOnly dateReportStart = TypeHelper.DateOnlyParse(dateReportStartTB.DateText);
            DateOnly dateReportEnd = TypeHelper.DateOnlyParse(dateReportEndTB.DateText);
            SaveDocument(dateReportStart, dateReportEnd);
        }

        private void FilepathTB_Click(object sender, RoutedEventArgs e)
        {
            OpenSaveFileDialog();
        }
    }
}
