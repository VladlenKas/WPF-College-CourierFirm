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
                // Меняем данные заказа
                order.DatetimeCompletion = DateTime.Now;
                dbContext.Update(order);
                dbContext.SaveChanges();

                // Создаем доставку
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

        // Принятие курьером
        public static void AcceptOrderCourier(Order order, Employee employee)
        {
            using (var dbContext = new CourierServiceContext())
            {
                // Меняем данные заказа
                order.DatetimeCompletion = DateTime.Now;
                dbContext.Update(order);
                dbContext.SaveChanges();

                // Создаем доставку
                Delivery delivery = new()
                {
                    Order = order,
                    StatusDeliveryId = 3 // в пути
                };

                dbContext.Add(delivery);
                dbContext.SaveChanges();

                // Привязываем курьера
                EmployeeDelivery employeeDelivery = new()
                {
                    EmployeeId = employee.EmployeeId,
                    DeliveryId = delivery.DeliveryId
                };

                dbContext.Add(employeeDelivery);
                dbContext.SaveChanges();
            }
        }
    }
}
