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

namespace WPF_CourierFirm.Components
{
    /// <summary>
    /// Логика взаимодействия для FilterDropdownPanel.xaml
    /// </summary>
    public partial class FilterDropdownPanelOrders : UserControl
    {

        public FilterDropdownPanelOrders()
        {
            InitializeComponent();
            allOrderParameterRB.IsChecked = true;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterPopup.IsOpen = !FilterPopup.IsOpen;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            allOrderParameterRB.IsChecked = true;
        }
    }
}
