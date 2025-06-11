using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFirm.Model;

namespace WPF_CourierFirm.Classes.Services
{
    public static class TransportService
    {
        // Добавление
        public static void AddTransport(Employee courier, string licensePlate, string brand,
            string model, string color, short year)
        {
            using (var dbContext = new CourierServiceContext())
            {
                Transport transport = new()
                {
                    LicensePlate = licensePlate,
                    Brand = brand,
                    Model = model,
                    Color = color,
                    Year = year
                };

                dbContext.Add(transport);
                dbContext.SaveChanges();

                courier.TransportId = transport.TransportId;
                dbContext.Update(courier);
                dbContext.SaveChanges();
            }
        }

        // Редактирование
        public static void EditTransport(Transport transport, string licensePlate, string brand,
            string model, string color, short year)
        {
            using (var dbContext = new CourierServiceContext())
            {
                transport.LicensePlate = licensePlate;
                transport.Brand = brand;
                transport.Model = model;
                transport.Color = color;
                transport.Year = year;
                dbContext.SaveChanges();

                dbContext.Update(transport);
                dbContext.SaveChanges();
            }
        }
    }
}
