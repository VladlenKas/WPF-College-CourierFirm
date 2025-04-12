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
using WPF_CourierFrim.Classes;

namespace WPF_CourierFrim.Components
{
    /// <summary>
    /// Логика взаимодействия для BindablePhoneBox.xaml
    /// </summary>
    public partial class BindablePhoneBox : UserControl
    {
        private bool _isUpdatingText;

        public static readonly DependencyProperty PhoneNumberProperty =
            DependencyProperty.Register("PhoneNumber", typeof(string), typeof(BindablePhoneBox),
                new FrameworkPropertyMetadata(string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPhoneNumberChanged));

        public string PhoneNumber
        {
            get
            {
                var cleanNumber = (string)GetValue(PhoneNumberProperty);
                return string.IsNullOrEmpty(cleanNumber)
                    ? string.Empty
                    : cleanNumber.Replace(" ", "").Replace("+", "");
            }
            set => SetValue(PhoneNumberProperty, value);
        }

        public BindablePhoneBox()
        {
            InitializeComponent();
        }

        private static void OnPhoneNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePhoneBox control && !control._isUpdatingText)
            {
                control.phoneBox.Text = e.NewValue?.ToString();
            }
        }

        private void PhoneBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingText) return;
            _isUpdatingText = true;

            var text = phoneBox.Text.Replace(" ", "").Replace("+", "");

            // Автодобавление +7 если пустое поле
            if (string.IsNullOrEmpty(text) && phoneBox.IsFocused)
            {
                text = "+7";
            }
            // Принудительно начинаем с +7
            else if (!text.StartsWith("7") || text.Length < 1)
            {
                text = "7" + text;
            }

            text = "+7" + text.Substring(1); // Фиксируем начало номера

            if (text.Length > 1)
            {
                var cleanNumber = text.Substring(2); // Убираем +7
                var formatted = FormatPhoneNumber(cleanNumber);
                PhoneNumber = formatted;
                phoneBox.Text = formatted;
                phoneBox.CaretIndex = formatted.Length;
            }

            _isUpdatingText = false;
        }

        private string FormatPhoneNumber(string number)
        {
            number = number.Length > 10 ? number.Substring(0, 10) : number;

            return number switch
            {
                var n when n.Length <= 3 => $"+7 {n}",
                var n when n.Length <= 6 => $"+7 {n.Substring(0, 3)} {n.Substring(3)}",
                var n when n.Length <= 8 => $"+7 {n.Substring(0, 3)} {n.Substring(3, 3)} {n.Substring(6)}",
                _ => $"+7 {number.Substring(0, 3)} {number.Substring(3, 3)} {number.Substring(6, 2)} {number.Substring(8)}"
            };
        }

        private void PhoneBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                var pos = phoneBox.SelectionStart;
                if (pos <= 3) // Блокируем удаление +7
                {
                    e.Handled = true;
                    return;
                }

                // Логика удаления с учетом пробелов
                if (phoneBox.Text[pos - 1] == ' ')
                {
                    phoneBox.Text = phoneBox.Text.Remove(pos - 2, 2);
                    phoneBox.SelectionStart = pos - 2;
                    e.Handled = true;
                }
            }
        }

        private void PhoneBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text == "+7" || string.IsNullOrWhiteSpace(phoneBox.Text.Replace("+7", "")))
            {
                _isUpdatingText = true;
                PhoneNumber = string.Empty;
                phoneBox.Text = string.Empty;
                _isUpdatingText = false;
            }
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Validations.ValidateInputNumbers(e);
        }

        private void PhoneBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            Validations.ValidatePasteNumbers(e);
        }
    }
}
