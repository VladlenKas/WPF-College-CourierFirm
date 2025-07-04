﻿using System;
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
using WPF_CourierFirm.Classes.Helpers;
using WPF_CourierFirm.Classes.Services;
using WPF_CourierFirm.DialogWindows;
using WPF_CourierFirm.Model;
using WPF_CourierFirm.WindowsDialog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WPF_CourierFirm.Pages.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для EmployeePageAdmin.xaml
    /// </summary>
    public partial class EmployeePageAdmin : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private readonly Employee _admin;
        private EmployeeDataService _employeeDataService;
        private Employee SelectedEmployee => (Employee)itemsDG.SelectedItem;

        // Конструктор
        public EmployeePageAdmin(Employee admin)
        {
            InitializeComponent();

            _dbContext = new();
            _admin = admin;
            _employeeDataService = new(filterCB, sorterCB, searchTB, ascendingCHB, searchBTN, resetFiltersBTN, UpdateDG);

            UpdateDG();
        }

        // Методы
        private void UpdateDG()
        {
            _dbContext = new();
            var employees = _dbContext.Employees.ToList();

            employees = _employeeDataService.ApplyFilter(employees);
            employees = _employeeDataService.ApplySort(employees);
            employees = _employeeDataService.ApplySearch(employees);

            itemsDG.ItemsSource = employees;
        }

        private bool IsCurrentAdmin()
        {
            if (_admin.EmployeeId == SelectedEmployee.EmployeeId)
            {
                MessageBox.Show("Администратор не может удалить самого себя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
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
            EditEmployeeWindow window = new(SelectedEmployee, _admin);
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (saved) UpdateDG();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool isCurrentAdmin = IsCurrentAdmin();
            if (isCurrentAdmin) return;

            bool delete = MessageHelper.ConfirmDelete();
            if (!delete) return;

            _dbContext = new();
            EmployeeService.DeleteEmployee(SelectedEmployee);

            UpdateDG();
        }
    }
}
