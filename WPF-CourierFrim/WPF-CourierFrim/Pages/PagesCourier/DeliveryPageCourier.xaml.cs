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
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Pages.PagesCourier
{
    /// <summary>
    /// Логика взаимодействия для DeliveryPageCourier.xaml
    /// </summary>
    public partial class DeliveryPageCourier : Page
    {
        // Поля и свойства
        private CourierServiceContext _dbContext;
        private Employee _thisEmpoyee;

        // Конструктор
        public DeliveryPageCourier(Employee employee)
        {
            InitializeComponent();
            _dbContext = new();
            _thisEmpoyee = employee;
        }
    }
}
