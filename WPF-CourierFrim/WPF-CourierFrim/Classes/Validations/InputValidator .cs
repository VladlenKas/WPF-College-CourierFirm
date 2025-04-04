﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace WPF_CourierFrim.Classes.DataOperations
{
    public static partial class InputValidator 
    {
        #region Валидация на написание текста

        /// <summary>
        /// Проверяет, что вводимый текст состоит только из кириллических символов.
        /// </summary>
        public static void ValidateInputCyrillic(TextCompositionEventArgs e)
        {
            ValidateInput(e, Cyrillic());
        }

        /// <summary>
        /// Проверяет, что вводимый текст состоит только из цифр.
        /// </summary>
        public static void ValidateInputNumbers(TextCompositionEventArgs e)
        {
            ValidateInput(e, Numbers());
        }

        /// <summary>
        /// Проверяет, что вводимый текст соответствует формату описания.
        /// </summary>
        public static void ValidateInputDescription(TextCompositionEventArgs e)
        {
            ValidateInput(e, Description());
        }

        /// <summary>
        /// Проверяет, что вводимый текст соответствует формату электронной почты.
        /// </summary>
        public static void ValidateInputEmail(TextCompositionEventArgs e)
        {
            ValidateInput(e, Email());
        }

        /// <summary>
        /// Проверяет, что вводимый текст соответствует формату пароля.
        /// </summary>
        public static void ValidateInputPassword(TextCompositionEventArgs e)
        {
            ValidateInput(e, Password());
        }

        #endregion

        #region Валидация на вставку текста

        /// <summary>
        /// Проверяет вставляемый текст на соответствие кириллическим символам.
        /// </summary>
        public static void ValidatePasteCyrillic(DataObjectPastingEventArgs e)
        {
            ValidatePaste(e, Cyrillic());
        }

        /// <summary>
        /// Проверяет вставляемый текст на соответствие числам.
        /// </summary>
        public static void ValidatePasteNumbers(DataObjectPastingEventArgs e)
        {
            ValidatePaste(e, Numbers());
        }

        /// <summary>
        /// Проверяет вставляемый текст на соответствие формату описания для адреса.
        /// </summary>
        public static void ValidatePasteDescriptionForAddress(DataObjectPastingEventArgs e)
        {
            ValidatePaste(e, Description());
        }

        /// <summary>
        /// Проверяет вставляемый текст на соответствие формату электронной почты.
        /// </summary>
        public static void ValidatePasteEmail(DataObjectPastingEventArgs e)
        {
            ValidatePaste(e, Email());
        }

        /// <summary>
        /// Проверяет вставляемый текст на соответствие формату пароля.
        /// </summary>
        public static void ValidatePastePassword(DataObjectPastingEventArgs e)
        {
            string pastedText = GetTextFromPaste(e);
            var regex = Password();
            if (!regex.IsMatch(pastedText))
            {
                e.CancelCommand();
            }
        }

        #endregion

        /// <summary>
        /// Извлекает текст из буфера обмена при вставке.
        /// </summary>
        private static string GetTextFromPaste(DataObjectPastingEventArgs e)
        {
            return e.DataObject.GetData(DataFormats.Text)?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Проверяет, соответствует ли вводимый текст заданному регулярному выражению.
        /// </summary>
        private static void ValidateInput(TextCompositionEventArgs e, Regex regex)
        {
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Блокируем ввод
            }
        }

        /// <summary>
        /// Проверяет, соответствует ли вставляемый текст заданному регулярному выражению.
        /// </summary>
        private static void ValidatePaste(DataObjectPastingEventArgs e, Regex regex)
        {
            string pastedText = GetTextFromPaste(e);
            if (!regex.IsMatch(pastedText))
            {
                e.CancelCommand(); // Блокируем вставку
            }
        }

        // Кирилика
        [GeneratedRegex(@"[а-яА-Я\-]")]
        private static partial Regex Cyrillic();

        // Цифры
        [GeneratedRegex(@"[0-9]")]
        private static partial Regex Numbers();

        // Описание
        [GeneratedRegex(@"[а-яА-Я\d-().,;""':/]")]
        private static partial Regex Description();

        // Эл. почта
        [GeneratedRegex(@"[a-zA-Z0-9\@\.]")]
        private static partial Regex Email();

        // Пароль
        [GeneratedRegex(@"[a-zA-Z!@#$&*0-9]")]
        private static partial Regex Password();
    }
}
