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
using System.Windows.Shapes;
using WPF_CourierFrim.Classes;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Classes.Services;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Windows.WindowsDialog
{
    /// <summary>
    /// Логика взаимодействия для EditRateWindow.xaml
    /// </summary>
    public partial class EditRateWindow : Window
    {
        // Поля и свойства
        public bool Saved { get; private set; }
        private Rate _rate;

        // Конструктор
        public EditRateWindow(Rate rate)
        {
            InitializeComponent();
            _rate = rate;

            nameTB.Text = rate.Name;
            costTB.Text = rate.Cost.ToString();
            descriptionTB.Text = rate.Description;
        }

        // Обработчики событий
        private void Exit_Click(object sender, RoutedEventArgs e) => MessageHelper.ConfirmExit(this);

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTB.Text;
            int cost = Convert.ToInt32(costTB.Text);
            string desciption = descriptionTB.Text;

            bool notError = Limitators.RateLimitator(null, name, cost, desciption);
            if (!notError) return;

            bool accept = MessageHelper.ConfirmEdit();
            if (!accept) return;

            RateService.EditRate(_rate, name, cost, desciption);
            Saved = true;
            Close();
        }
    }
}
