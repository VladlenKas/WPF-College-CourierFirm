using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPF_CourierFirm.Classes.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.RegisterAttached("IconKind", typeof(PackIconKind), typeof(TextBoxHelper));

        public static void SetIconKind(TextBox element, PackIconKind value)
            => element.SetValue(IconKindProperty, value);

        public static PackIconKind GetIconKind(TextBox element)
            => (PackIconKind)element.GetValue(IconKindProperty);
    }
}
