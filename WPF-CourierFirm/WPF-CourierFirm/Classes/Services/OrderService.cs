﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using WPF_CourierFirm.Model;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CourierFirm.Classes.Services
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
        public static void CreateOrder(Organisation organisation, Rate rate,
            string receivingAddress, string deliveryAddress, ContentType contentType, string phoneClient,
            string fullnameClient, string descriptionContent, decimal weight)
        {
            using (var dbContext = new CourierServiceContext())
            {
                // Создаем содержимое заказа
                Content content = new()
                {
                    Description = descriptionContent,
                    Weight = weight,
                    ContentTypeId = contentType.ContentTypeId
                };
                dbContext.Add(content);
                dbContext.SaveChanges();

                // Создаем сам заказ
                Order order = new()
                {
                    OrganisationId = organisation.OrganisationId,
                    RateId = rate.RateId,
                    ContentId = content.ContentId,
                    ReceivingAddress = receivingAddress,
                    DeliveryAddress = deliveryAddress,
                    PhoneClient = phoneClient,
                    FullnameClient = fullnameClient,
                    DatetimeCreation = DateTime.Now
                };
                dbContext.Add(order);
                dbContext.SaveChanges();
            }
        }

        // Редактирование
        public static void EditOrder(Order order, Organisation organisation, Rate rate,
            string receivingAddress, string deliveryAddress, ContentType contentType, string phoneClient,
            string fullnameClient, string descriptionContent, decimal weight)
        {
            using (var dbContext = new CourierServiceContext())
            {
                // Получаем текущий заказ из базы с отслеживанием
                var existingOrder = dbContext.Orders
                    .Include(o => o.Content) 
                    .Single(o => o.OrderId == order.OrderId);

                // Обновляем содержимое
                existingOrder.Content.Description = descriptionContent;
                existingOrder.Content.Weight = weight;
                existingOrder.Content.ContentTypeId = contentType.ContentTypeId;

                // Обновляем сам заказ
                existingOrder.OrganisationId = organisation.OrganisationId;
                existingOrder.RateId = rate.RateId;
                existingOrder.ReceivingAddress = receivingAddress;
                existingOrder.DeliveryAddress = deliveryAddress;
                existingOrder.PhoneClient = phoneClient;
                existingOrder.FullnameClient = fullnameClient;

                // Сохраняем изменения
                dbContext.SaveChanges();
            }
        }

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
