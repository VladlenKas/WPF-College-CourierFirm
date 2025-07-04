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
using WPF_CourierFirm.Model;

namespace WPF_CourierFirm.UserControls.CardsCourier
{
    /// <summary>
    /// Логика взаимодействия для CardRateCourier.xaml
    /// </summary>
    public partial class CardRateCourier : UserControl
    {
        // Поля 
        private CourierServiceContext _dbContext;
        private Rate _rate;

        // Конструктор
        public CardRateCourier(Rate rate)
        {
            InitializeComponent();

            _rate = rate;
            LoadInfo();
        }

        // Методы
        private void LoadInfo()
        {
            _dbContext = new();
            _dbContext.Attach(_rate);
            DataContext = _rate;
        }
    }
}