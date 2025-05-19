using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_CourierFrim.Components;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Helpers
{
    /// <summary>
    /// Класс для работы с компонентами
    /// </summary>
    public static class ComponentsHelper
    {
        /// <summary>
        /// Скрывает пароль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="passHid"></param>
        /// <param name="passVis"></param>
        public static void ToggleVisibilityPassword(object sender, BindablePasswordBox passHid, TextBox passVis)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.IsChecked == true)
            {
                // Vissible pass
                passVis.Text = passHid.Password;
                passVis.Visibility = Visibility.Visible;
                passHid.Visibility = Visibility.Hidden;
            }
            else
            {
                // Hidden pass
                passHid.Password = passVis.Text;
                passVis.Visibility = Visibility.Hidden;
                passHid.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Управляет видимость кнопок для смены статуса доставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="passHid"></param>
        /// <param name="passVis"></param>
        public static void ToggleVisibilityButtonsDelivery
            (Delivery delivery, Button getOrderBTN, Button handingOrderBTN,
            Button? cancellationDeliveryBTN)
        {
            if (delivery.StatusDelivery.Name == "В пути (за заказом)")
            {
                getOrderBTN.Visibility = Visibility.Visible;
                handingOrderBTN.Visibility = Visibility.Hidden;
            }
            else if (delivery.StatusDelivery.Name == "В пути (к клиенту)")
            {
                getOrderBTN.Visibility = Visibility.Hidden;
                handingOrderBTN.Visibility = Visibility.Visible;
            }
            else
            {
                getOrderBTN.Visibility = Visibility.Hidden;
                handingOrderBTN.Visibility = Visibility.Hidden;

                if (cancellationDeliveryBTN != null)
                    cancellationDeliveryBTN.IsEnabled = false;
            }
        }

        /// <summary>
        /// Быстро получает пароль
        /// </summary>
        /// <param name="passHid"></param>
        /// <param name="passVis"></param>
        /// <returns></returns>
        public static string GetPassword(BindablePasswordBox passHid, TextBox passVis)
        {
            var pass = passVis.Visibility is Visibility.Visible ? passVis.Text : passHid.Password;
            return pass;
        }

        /// <summary>
        /// Затемняет область экрана при открытии информации
        /// </summary>
        /// <param name="infoWindow"></param>
        /// <param name="page"></param>
        public static void ShowDialogWindowDark(Window infoWindow)
        {
            App.MenuWindow.Opacity = 0.5;
            infoWindow.ShowDialog();
            App.MenuWindow.Opacity = 1;
        }
    }
}
