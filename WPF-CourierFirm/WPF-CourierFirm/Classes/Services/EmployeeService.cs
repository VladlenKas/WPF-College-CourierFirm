using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Model;

namespace WPF_CourierFrim.Classes.Services
{
    public static class EmployeeService
    {
        // Удаление
        public static void DeleteEmployee(Employee employee)
        {
            using (var dbContext = new CourierServiceContext())
            {
                employee.IsDeleted = 1;
                dbContext.Update(employee);
                dbContext.SaveChanges();
            }
        }

        // Добавление
        public static void AddEmployee(string firstname, string lastname, string patronymic,
            DateOnly birthday, string phone, string passport, string login, string password, int postId)
        {
            using (var dbContext = new CourierServiceContext())
            {   
                Employee employee = new()
                {
                    Firstname = firstname,
                    Lastname = lastname,
                    Patronymic = patronymic,
                    Birthday = birthday,
                    Passport = passport,
                    Phone = phone,
                    Login = login,
                    Password = password,
                    PostId = postId
                };

                dbContext.Add(employee);
                dbContext.SaveChanges();
            }
        }

        // Редактирование
        public static void EditEmployee(Employee employee, string firstname, string lastname, string patronymic,
            DateOnly birthday, string phone, string passport, string login, string password, int postId)
        {
            using (var dbContext = new CourierServiceContext())
            {
                employee.Firstname = firstname;
                employee.Lastname = lastname;
                employee.Patronymic = patronymic;
                employee.Birthday = birthday;
                employee.Passport = passport;
                employee.Phone = phone;
                employee.Login = login;
                employee.Password = password;
                employee.PostId = postId;
                dbContext.SaveChanges();

                dbContext.Update(employee);
                dbContext.SaveChanges();
            }
        }
    }
}
