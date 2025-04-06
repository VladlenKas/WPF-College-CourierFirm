using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF_CourierFrim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Окно текущего меню
        public static Window MenuWindow { get; set; }

        // Открытая страница
        public static Window CurrentNavWindow { get; set; }

    }

}
