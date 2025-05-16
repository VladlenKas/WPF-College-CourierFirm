using LiveCharts.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        // Возвращает список выполненных заказов по месяцам для графика (для админа)
        public static List<DeliveryStatistic> GetMonthlyCompletedDeliveries(int year)
        {
            var dbContext = new CourierServiceContext();

            var deliveries = dbContext.AllDeliveries
                .Where(d => d.StatusDeliveryId == 5)
                .AsEnumerable()
                .Where(d => d.DatetimePresentation.Value.Year == year)
                .GroupBy(d => new
                {
                    Year = d.DatetimePresentation.Value.Year,
                    Month = d.DatetimePresentation.Value.Month
                })
                .Select(g => new DeliveryStatistic
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    MonthYear = $"{GetMonthName(g.Key.Month)} {g.Key.Year}",
                    CompletedDeliveries = g.Count()
                })
            .ToList();

            return FillMissingMonths(deliveries, year);
        }
        
        // Возвращает список выполненных заказов по месяцам для графика (для статистики курьера)
        public static List<DeliveryStatistic> GetMonthlyCompletedDeliveries(int year, Employee courier)
        {
            var dbContext = new CourierServiceContext();

            var deliveries = dbContext.AllDeliveries
                .Include(r => r.EmployeeDeliveries)
                    .ThenInclude(e => e.Employee)
                .Where(d => d.StatusDeliveryId == 5)
                .AsEnumerable()
                .Where(d => d.DatetimePresentation.Value.Year == year)
                .Where(r => r.EmployeeDeliveries
                    .Any(f => f.EmployeeId == courier.EmployeeId))
                .GroupBy(d => new
                {
                    Year = d.DatetimePresentation.Value.Year,
                    Month = d.DatetimePresentation.Value.Month
                })
                .Select(g => new DeliveryStatistic
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    MonthYear = $"{GetMonthName(g.Key.Month)} {g.Key.Year}",
                    CompletedDeliveries = g.Count()
                })
            .ToList();

            return FillMissingMonths(deliveries, year);
        }
        
        // Дает название каджому месяцу
        private static string GetMonthName(int month)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        }

        // Заполняет пропуски нулями 
        private static List<DeliveryStatistic> FillMissingMonths(List<DeliveryStatistic> data, int yearFilter)
        {
            var result = new List<DeliveryStatistic>(); // экземпляр класса DeliveryStatistic
            var months = Enumerable.Range(1, 12);       // месяцы
            int year = yearFilter;

            foreach (var month in months)
            {
                var stat = data.FirstOrDefault(d =>
                d.Year == year &&
                d.Month == month);

                result.Add(stat ?? new DeliveryStatistic
                {
                    Year = year,
                    Month = month,
                    MonthYear = $"{GetMonthName(month)} {year}",
                    CompletedDeliveries = 0.1
                });
            }

            return result;
        }
    }
}
