using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CourierFrim.Classes
{
    public static class Limitators
    {
        // Тарифы
        public static bool RateLimitator(Rate? rate, string name, int cost, string description)
        {
            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { name, description }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (cost == 0)
            {
                MessageHelper.MessageNullCost();
                return false;
            }
            // Проверка на дубликаты и внесение изменений
            else
            {
                // Редактирование
                if (rate != null)
                {
                    if (dbContext.Rates.Any(r => r.Name == name && r.RateId != rate.RateId))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Rates.Any(r => r.Description == description && r.RateId != rate.RateId))
                    {
                        MessageHelper.MessageDuplicateDescription();
                        return false;
                    }
                    else if (rate.Name == name && rate.Cost == cost && rate.Description == description)
                    {
                        MessageHelper.MessageNotChanges();
                        return false;
                    }
                }
                // Добавление
                else
                {
                    if (dbContext.Rates.Any(r => r.Name == name))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Rates.Any(r => r.Description == description))
                    {
                        MessageHelper.MessageDuplicateDescription();
                        return false;
                    }
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }

        // Организации 
        public static bool OrgLimitator(Organisation? organisation, string name, 
            string email, string phone, string address)
        {

            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { name, email, phone, address }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (name.Length < 2)
            {
                MessageHelper.MessageShortName();
                return false;
            }
            else if (phone.Length != 11)
            {
                MessageHelper.MessageShortPhone();
                return false;
            }
            else if (!Validations.ValidateCorrectEmail(email))
            {
                MessageHelper.MessageIncorrectEmail();
                return false;
            }
            else if (address.Length < 4)
            {
                MessageHelper.MessageShortAddress();
                return false;
            }
            // Проверка на дубликаты и внесение изменений
            else
            {
                // Редактирование
                if (organisation != null)
                {
                    if (dbContext.Organisations.Any(r => r.Name == name && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Email == email && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateEmail();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Phone == phone && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicatePhone();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Address == address && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateAddress();
                        return false;
                    }
                    else if (organisation.Name == name && organisation.Phone == phone && organisation.Email == email
                        && organisation.Address == address)
                    {
                        MessageHelper.MessageNotChanges();
                        return false;
                    }
                }
                // Добавление
                else
                {
                    if (dbContext.Organisations.Any(r => r.Name == name))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Email == email))
                    {
                        MessageHelper.MessageDuplicateEmail();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Phone == phone))
                    {
                        MessageHelper.MessageDuplicatePhone();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Address == address))
                    {
                        MessageHelper.MessageDuplicateAddress();
                        return false;
                    }
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }
    }
}
