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
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Windows
{
    /// <summary>
    /// Логика взаимодействия для NavWindowAdmin.xaml
    /// </summary>
    public partial class NavWindowAdmin : Window
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public NavWindowAdmin(Employee employee)
        {
            InitializeComponent();

            _dbContext = new();
            Title = $"Меню Администратора. Сотрудник: {employee.Fullname}";

            _thisEmpoyee = employee;
        }
    }
}
