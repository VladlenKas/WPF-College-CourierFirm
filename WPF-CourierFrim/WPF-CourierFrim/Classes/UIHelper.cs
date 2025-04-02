using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using WPF_CourierFirm.Components;

namespace WPF_CourierFrim.Classes
{
    // Класс для работы с паролем
    public static class PasswordHelper
    {
        /// <summary>
        /// Скрывает пароль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="passHid"></param>
        /// <param name="passVis"></param>
        public static void ToggleVisibility(object sender, BindablePasswordBox passHid, TextBox passVis)
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
    }

    // Класс для диалоговых окон
    public static class DialogHelper
    {
        /// <summary>
        /// Вызывает сообщение с подтверждением о выходе/закрытии окна
        /// </summary>
        /// <returns></returns>
        public static void ConfirmExit(Window window)
        {
            var resultChanged = MessageBox.Show("Вы действительно хотите выйти?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultChanged == MessageBoxResult.Yes)
            {
                window.Close();
            }
        }
    }

    // Класс для уведомлений
    public static class MessageHelper
    {
        /// <summary>
        /// Вызывает сообщение с подтверждением о выходе/закрытии окна
        /// </summary>
        /// <returns></returns>
        public static void NullFields()
        {
            MessageBox.Show($"Заполните все поля.", "Предупреждение.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    // Класс для работы с изображениями
    public static class ImageHelper
    {

        public static bool TrySelectImage(out string filePath)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
                return true;
            }
            filePath = null;
            return false;
        }

        /// <summary>
        /// Открывает файловый менджер для выбора изображений
        /// </summary>
        /// <param name="image"></param>
        public static bool OpenImageDialog(Image image, byte[]? originalImage, bool imageIsEditing)
        {
            var selectImage = new OpenFileDialog();
            selectImage.Filter = "Image files (*.jpg, *.jpeg, *.png *.webp)|*.jpg;*.jpeg;*.png;*.webp;";
            selectImage.InitialDirectory = @"C:\Users";
            if (selectImage.ShowDialog() == true)
            {
                string selectedFilePath = selectImage.FileName;
                image.Source = new BitmapImage(new Uri(selectedFilePath));
                return true; // уведомляем об изменении
            }

            // Если изображения не было, а потом изменилось
            if (image.Source != null && originalImage == null && imageIsEditing == false)
            {
                return true; // уведомляем об изменении
            }
            // Если изображения не было, но менялось, а потом снова удалили
            else if (image.Source == null && originalImage == null && imageIsEditing == true)
            {
                return true; // уведомляем об изменении
            }
            // Если изображение изменилось
            else if (imageIsEditing)
            {
                return true; // уведомляем об изменении
            }
            return false; // изменений нет
        }
    }

    public static class UIHelper
    {

        /// <summary>
        /// Открывает файловый менджер для выбора изображений
        /// </summary>
        /// <param name="image"></param>
        public static bool OpenImage(Image image, byte[]? originalImage, bool imageIsEditing)
        {
            var selectImage = new OpenFileDialog();
            selectImage.Filter = "Image files (*.jpg, *.jpeg, *.png *.webp)|*.jpg;*.jpeg;*.png;*.webp;";
            selectImage.InitialDirectory = @"C:\Users";
            if (selectImage.ShowDialog() == true)
            {
                string selectedFilePath = selectImage.FileName;
                image.Source = new BitmapImage(new Uri(selectedFilePath));
                return true; // уведомляем об изменении
            }

            // Если изображения не было, а потом изменилось
            if (image.Source != null && originalImage == null && imageIsEditing == false)
            {
                return true; // уведомляем об изменении
            }
            // Если изображения не было, но менялось, а потом снова удалили
            else if (image.Source == null && originalImage == null && imageIsEditing == true)
            {
                return true; // уведомляем об изменении
            }
            // Если изображение изменилось
            else if (imageIsEditing)
            {
                return true; // уведомляем об изменении
            }
            return false; // изменений нет
        }

        /// <summary>
        /// Конвертирует изображение (Image) в массив байтов (byte[])
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static byte[]? ImageSourceToBytes(ImageSource? imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }

            byte[]? bytes = null;

            var bitmapSource = imageSource as BitmapSource;
            if (bitmapSource != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }
    }
}
