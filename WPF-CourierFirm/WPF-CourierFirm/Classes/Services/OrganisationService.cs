using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WPF_CourierFrim.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CourierFrim.Classes.Services
{
    public static class OrganisationService
    {

        // Удаление
        public static void DeleteOrganisation(Organisation organisation)
        {
            using (var dbContext = new CourierServiceContext())
            {
                organisation.IsDeleted = 1;
                dbContext.Update(organisation);
                dbContext.SaveChanges();
            }
        }

        // Добавление
        public static void AddOgranisation(string name, string email, string phone, string address)
        {
            using (var dbContext = new CourierServiceContext())
            {
                Organisation organisation = new()
                {
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Email = email
                };

                dbContext.Add(organisation);
                dbContext.SaveChanges();
            }
        }

        // Редактирование
        public static void EditOgranisation(Organisation organisation, string name, string email, string phone, string address)
        {
            using (var dbContext = new CourierServiceContext())
            {
                organisation.Name = name;
                organisation.Address = address;
                organisation.Phone = phone;
                organisation.Email = email;

                dbContext.Update(organisation);
                dbContext.SaveChanges();
            }
        }
    }
}
