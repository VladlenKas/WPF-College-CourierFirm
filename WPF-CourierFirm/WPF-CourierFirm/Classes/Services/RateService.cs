using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFirm.Model;

namespace WPF_CourierFirm.Classes.Services
{
    public static class RateService
    {
        // Удаление
        public static void DeleteRate(Rate rate)
        {
            using (var dbContext = new CourierServiceContext())
            {
                rate.IsDeleted = 1;
                dbContext.Update(rate);
                dbContext.SaveChanges();
            }
        }

        // Добавление
        public static void AddRate(string name, int cost, string description)
        {
            using (var dbContext = new CourierServiceContext())
            {
                Rate rate = new()
                {
                    Name = name,
                    Cost = cost,
                    Description = description
                };

                dbContext.Add(rate);
                dbContext.SaveChanges();
            }
        }

        // Редактирование
        public static void EditRate(Rate rate, string name, int cost, string description)
        {
            using (var dbContext = new CourierServiceContext())
            {
                rate.Name = name;
                rate.Cost = cost;
                rate.Description = description;

                dbContext.Update(rate);
                dbContext.SaveChanges();
            }
        }
    }
}
