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
        // Отмена доставки
        public static void CancellationDelivery(Delivery delivery)
        {
            using (var dbContext = new CourierServiceContext())
            {
                dbContext.Attach(delivery);

                delivery.StatusDeliveryId = 2; // отмена доставки
                delivery.DatetimePresentation = DateTime.Now;

                dbContext.Update(delivery);
                dbContext.SaveChanges();
            }
        }

        // Получение заказа
        public static void GetOrder(Delivery delivery)
        {
            using (var dbContext = new CourierServiceContext())
            {
                dbContext.Attach(delivery);

                delivery.StatusDeliveryId = 4; // заказ получен
                delivery.DatetimeReceiving = DateTime.Now;

                dbContext.Update(delivery);
                dbContext.SaveChanges();
            }
        }

        // Вручение заказа
        public static void PaymentMethodDelivery(Delivery delivery, int paymentMethod)
        {
            using (var dbContext = new CourierServiceContext())
            {
                dbContext.Attach(delivery);

                delivery.PaymentMethod = (sbyte)paymentMethod;

                dbContext.Update(delivery);
                dbContext.SaveChanges();
            }
        }

        // Добавление способа оплаты
        public static void HandingOrder(Delivery delivery)
        {
            using (var dbContext = new CourierServiceContext())
            {
                dbContext.Attach(delivery);

                delivery.StatusDeliveryId = 5; // заказ вручен
                delivery.DatetimePresentation = DateTime.Now;

                dbContext.Update(delivery);
                dbContext.SaveChanges();
            }
        }

        // Редактирование

        // Принятие курьером
    }
}
