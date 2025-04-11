using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Model;

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

    }
}
