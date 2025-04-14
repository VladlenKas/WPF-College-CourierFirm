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

namespace WPF_CourierFrim.Components
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _isPasswordChanged;

        // Using a DependencyProperty as the backing store for PasswordProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), 
                new FrameworkPropertyMetadata(string.Empty, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanged = true;
            Password = passwordBox.Password;
            _isPasswordChanged = false;
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanged)
            {
                passwordBox.Password = Password; 
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            icon.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(160, 165, 175));
            icon.Foreground = new SolidColorBrush(Color.FromRgb(160, 165, 175));
        }
    }
}
