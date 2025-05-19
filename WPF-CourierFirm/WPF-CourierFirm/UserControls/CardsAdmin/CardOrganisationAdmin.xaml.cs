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
using WPF_CourierFrim.DialogWindows;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.WindowsDialog;

namespace WPF_CourierFrim.UserControls.CardsAdmin
{
    /// <summary>
    /// Логика взаимодействия для CardOrganisationAdmin.xaml
    /// </summary>
    public partial class CardOrganisationAdmin : UserControl
    {
        // Поля 
        public Organisation Organisation { get; private set; }
        private CourierServiceContext _dbContext;
        private Organisation _organisation;

        // Конструктор
        public CardOrganisationAdmin(Organisation organisation)
        {
            InitializeComponent();

            _organisation = organisation;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_organisation);
            DataContext = _organisation;
        }

        // Обработчики событий
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bool delete = MessageHelper.ConfirmDelete();
            if (!delete) return;

            _dbContext = new();
            OrganisationService.DeleteOrganisation(_organisation);

            OrgRequested?.Invoke(this, new OrgEventArgs { Organisation = this.Organisation }); // Уведомляем род. страницу об удалении
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditOrgWindow window = new(_organisation);
            ComponentsHelper.ShowDialogWindowDark(window);

            bool saved = window.Saved;
            if (!saved) return;

            _dbContext = new();
            OrgRequested?.Invoke(this, new OrgEventArgs { Organisation = this.Organisation }); // Уведомляем род. страницу об удалении
        }

        // События
        public event EventHandler<OrgEventArgs> OrgRequested;
        public class OrgEventArgs : EventArgs
        {
            public Organisation Organisation { get; set; }
        }
    }
}