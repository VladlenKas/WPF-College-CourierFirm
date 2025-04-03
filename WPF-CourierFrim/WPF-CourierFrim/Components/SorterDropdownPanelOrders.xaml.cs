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
    /// Логика взаимодействия для SorterDropdownPanelOrders.xaml
    /// </summary>
    public partial class SorterDropdownPanelOrders : UserControl
    {
        public SorterDropdownPanelOrders()
        {
            InitializeComponent();
            dateAscParameterRB.IsChecked = true;
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            SortPopup.IsOpen = !SortPopup.IsOpen;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            dateAscParameterRB.IsChecked = true;
        }
    }
}
