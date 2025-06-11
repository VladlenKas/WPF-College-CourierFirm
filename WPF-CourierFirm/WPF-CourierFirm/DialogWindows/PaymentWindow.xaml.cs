using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
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
using System.Windows.Shapes;
using WPF_CourierFirm.Classes;
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.Model;

namespace WPF_CourierFirm.WindowsDialog
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        public int PaymentMethod => paymentCB.SelectedIndex;

        private CourierServiceContext dbContext;
        private Delivery _delivery;
        private string _filepath;

        // Конструктор
        public PaymentWindow(Delivery delivery)
        {
            InitializeComponent();
            _delivery = delivery;
            Saved = false;

            DataContext = _delivery;
            paymentCB.ItemsSource = new List<string> { "Наличный расчет", "Оплата картой" };
        }

        // Методы
        private bool CheckFields()
        {
            if (PaymentMethod == -1)
            {
                MessageHelper.MessageNullFields();  // Показать предупреждение
                return false;
            }
            else if (string.IsNullOrEmpty(_filepath))
            {
                MessageHelper.MessageNullFilepath();  // Показать предупреждение
                return false;
            }
            else
            {
                return true;
            }
        }
        private void OpenSaveFileDialog()
        {
            // Выбор пути (вызов проводника)
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Pdf Files|*.pdf*",
                Title = "Сохранить PDF документ",
                FileName = $"Чек об оказании услуг доставки №{_delivery.DeliveryId}"
            };

            // Если пользователь выбрал путь для сохранения чека
            if (saveFileDialog.ShowDialog() == true)
            {
                _filepath = $"{saveFileDialog.FileName}.pdf"; // Путь для открытия файла
                filepathTB.Text = $"Путь к документу: {_filepath}";
            }
        }

        private void SaveDocument()
        {
            DocumentService.GenerateReceipt(_filepath, _delivery);

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

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool nullFields = CheckFields();
            if (!nullFields) return;

            bool accept = MessageHelper.ConfirmChangeStatus();
            if (!accept) return;

            DeliveryService.PaymentMethodDelivery(_delivery, PaymentMethod);
            DeliveryService.HandingOrder(_delivery);

            dbContext = new();
            dbContext.Attach(_delivery);
            SaveDocument();

            Saved = true;
            Close();
        }

        private void FilepathTB_Click(object sender, RoutedEventArgs e)
        {
            OpenSaveFileDialog();
        }
    }
}
