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
using WPF_CourierFrim.Windows.DialogWindows;

namespace WPF_CourierFrim.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для EmployeePageAdmin.xaml
    /// </summary>
    public partial class EmployeePageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _admin;
        private Employee _selectedEmployee => (Employee)itemsDG.SelectedItem;

        // Конструктор
        public EmployeePageAdmin(Employee admin)
        {
            InitializeComponent();
            _dbContext = new();
            _admin = admin;

            // Загрузка комбобоксов и тд

            UpdateDG();
        }

        // Методы
        private void UpdateDG()
        {
            _dbContext = new();
            var employees = _dbContext.Employees.ToList();

            // Фильтрация и сортировка 

            itemsDG.ItemsSource = employees;
        }

        // Обработчики событий
        private void AddEmp_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow window = new();
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateDG();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow window = new(_selectedEmployee, _admin);
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateDG();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool delete = MessageHelper.ConfirmDelete();
            if (!delete) return;

            _dbContext = new();
            EmployeeService.DeleteEmployee(_selectedEmployee);
        }
    }
}
