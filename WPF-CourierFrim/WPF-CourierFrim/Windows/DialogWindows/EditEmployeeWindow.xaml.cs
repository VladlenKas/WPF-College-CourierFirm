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

namespace WPF_CourierFrim.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private CourierServiceContext dbContext;
        private Employee _employee;

        // Конструктор
        public EditEmployeeWindow(Employee employee, Employee admin)
        {
            InitializeComponent();
            dbContext = new();
            _employee = employee;

            postCB.ItemsSource = dbContext.Posts.ToList();

            firstnameTB.Text = employee.Firstname;
            lastnameTB.Text = employee.Lastname;
            patronymicTB.Text = employee.Patronymic;
            dateTB.DateText = employee.Birthday.ToString();
            phoneTB.PhoneNumber = employee.Phone;
            passportTB.Text = employee.Passport;
            loginTB.Text = employee.Login;
            PassTB.Text = employee.Password;
            PassTB.Text = employee.Password;
            postCB.SelectedItem = dbContext.Posts.Single(r => r.PostId == _employee.PostId);

            // ограничиваем изменения логина у текущего админа
            if (admin.EmployeeId == employee.EmployeeId)
            {
                loginTB.IsEnabled = false;
            }
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string firstname = firstnameTB.Text;
            string lastname = lastnameTB.Text;
            string patronymic = patronymicTB.Text;
            DateOnly birthday = TypeHelper.DateOnlyParse(dateTB.DateText);
            string phone = phoneTB.PhoneNumber;
            string passport = passportTB.Text;
            string login = loginTB.Text;
            string password = ComponentsHelper.GetPassword(PassPB, PassTB);
            Post? post = (Post)postCB.SelectedItem;

            bool notError = Limitators.EmployeeLimitator(_employee, firstname, lastname, patronymic, birthday,
                phone, passport, login, password, post);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmEdit();
            if (!accept) return;

            EmployeeService.EditEmployee(_employee, firstname, lastname, patronymic, birthday,
                phone, passport, login, password, post.PostId);
            Saved = true;
            Close();
        }

        private void VisibilityPassword_Click(object sender, RoutedEventArgs e)
        {
            ComponentsHelper.ToggleVisibilityPassword(sender, PassPB, PassTB);  // Переключить видимость пароля
        }
    }
}