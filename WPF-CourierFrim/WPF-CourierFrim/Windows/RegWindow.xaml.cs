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
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {

        // Поля и свойства
        private CourierServiceContext _dbContext;
        private string Login => loginTB.Text;
        private string Password => WindowHelper.GetPassword(PassPB, PassTB);

        public RegWindow()
        {
            InitializeComponent();
            _dbContext = new();
        }

        // Методы
        private void Reg()
        {

        }

        // Тригеры

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Reg();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new();
            authWindow.Show();
            this.Close();
        }

        private void VisibilityPassword_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.VisibilityPassword(sender, PassPB, PassTB);
        }
    }
}
