using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Services
{
    public static class OrderService
    {
        // Удаление
        public static void DeleteOrder(Order order)
        {
            using (var dbContext = new CourierServiceContext())
            {
                order.DatetimeCompletion = DateTime.Now;
                dbContext.Update(order);
                dbContext.SaveChanges();

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
