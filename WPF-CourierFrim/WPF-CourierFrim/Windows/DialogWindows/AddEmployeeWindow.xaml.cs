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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private CourierServiceContext dbContext;

        // Конструктор
        public AddEmployeeWindow()
        {
            InitializeComponent();
            dbContext = new();

            postCB.ItemsSource = dbContext.Posts.ToList();
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Add_Click(object sender, RoutedEventArgs e)
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

            bool notError = Limitators.EmployeeLimitator(null, firstname, lastname, patronymic, birthday, 
                phone, passport, login, password, post);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmSave();
            if (!accept) return;

            EmployeeService.AddEmployee(firstname, lastname, patronymic, birthday,
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