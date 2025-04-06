using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Services
{
    public static class DeliveryService
    {
        // Удаление заказа
        public static void DeleteDeliveryWithoutEmp(Order order)
        {
            using (var dbContext = new CourierServiceContext())
            {
                Delivery delivery = new()
                {
                    Order = order,
                    StatusDeliveryId = 1 // отмена заказа
                };

                dbContext.Add(delivery);
                dbContext.SaveChanges();
            }
        }

        // Добавление

        // Редактирование
    }
}
