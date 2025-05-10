using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPF_CourierFrim.Classes.Helpers;
using WPF_CourierFrim.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CourierFrim.Classes
{
    public static class Limitators
    {
        // Тарифы
        public static bool RateLimitator(Rate? rate, string name, int cost, string description)
        {
            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { name, description }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (cost == 0)
            {
                MessageHelper.MessageNullCost();
                return false;
            }
            // Проверка на дубликаты и внесение изменений
            else
            {
                // Редактирование
                if (rate != null)
                {
                    if (dbContext.Rates.Any(r => r.Name == name && r.RateId != rate.RateId))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Rates.Any(r => r.Description == description && r.RateId != rate.RateId))
                    {
                        MessageHelper.MessageDuplicateDescription();
                        return false;
                    }
                    else if (rate.Name == name && rate.Cost == cost && rate.Description == description)
                    {
                        MessageHelper.MessageNotChanges();
                        return false;
                    }
                }
                // Добавление
                else
                {
                    if (dbContext.Rates.Any(r => r.Name == name))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Rates.Any(r => r.Description == description))
                    {
                        MessageHelper.MessageDuplicateDescription();
                        return false;
                    }
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }

        // Организации 
        public static bool OrgLimitator(Organisation? organisation, string name,
            string email, string phone, string address)
        {
            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { name, email, phone, address }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (name.Length <= 2)
            {
                MessageHelper.MessageShortName();
                return false;
            }
            else if (phone.Length != 11)
            {
                MessageHelper.MessageShortPhone();
                return false;
            }
            else if (!Validations.ValidateCorrectEmail(email))
            {
                MessageHelper.MessageIncorrectEmail();
                return false;
            }
            else if (address.Length <= 4)
            {
                MessageHelper.MessageShortAddress();
                return false;
            }
            // Проверка на дубликаты и внесение изменений
            else
            {
                // Редактирование
                if (organisation != null)
                {
                    if (dbContext.Organisations.Any(r => r.Name == name && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Email == email && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateEmail();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Phone == phone && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicatePhone();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Address == address && r.OrganisationId != organisation.OrganisationId))
                    {
                        MessageHelper.MessageDuplicateAddress();
                        return false;
                    }
                    else if (organisation.Name == name && organisation.Phone == phone && organisation.Email == email
                        && organisation.Address == address)
                    {
                        MessageHelper.MessageNotChanges();
                        return false;
                    }
                }
                // Добавление
                else
                {
                    if (dbContext.Organisations.Any(r => r.Name == name))
                    {
                        MessageHelper.MessageDuplicateName();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Email == email))
                    {
                        MessageHelper.MessageDuplicateEmail();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Phone == phone))
                    {
                        MessageHelper.MessageDuplicatePhone();
                        return false;
                    }
                    else if (dbContext.Organisations.Any(r => r.Address == address))
                    {
                        MessageHelper.MessageDuplicateAddress();
                        return false;
                    }
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }

        // Заказы 
        public static bool OrderLimitator(Order? order, Organisation? organisation, Rate? rate,
            string receivingAddress, string deliveryAddress, ContentType? contentType, string phoneClient,
            string fullnameClient, string content, decimal weight)
        {
            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { receivingAddress, deliveryAddress, fullnameClient, phoneClient, content }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (organisation == null || rate == null || contentType == null)
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (fullnameClient.Length <= 2)
            {
                MessageHelper.MessageShortName();
                return false;
            }
            else if (phoneClient.Length != 11)
            {
                MessageHelper.MessageShortPhone();
                return false;
            }
            else if (receivingAddress.Length <= 4)
            {
                MessageHelper.MessageShortAddress();
                return false;
            }
            else if (deliveryAddress.Length <= 4)
            {
                MessageHelper.MessageShortAddress();
                return false;
            }
            else if (content.Length <= 4)
            {
                MessageHelper.MessageShortContentOrder();
                return false;
            }
            else if (weight == 0)
            {
                MessageHelper.MessageNullWeight();
                return false;
            }
            // Проверка на внесение изменений
            else if (order != null)
            {
                if (order.Organisation.Name == organisation.Name && order.Rate.Name == rate.Name && order.ReceivingAddress == receivingAddress
                    && order.DeliveryAddress == deliveryAddress && order.FullnameClient == fullnameClient && order.PhoneClient == phoneClient
                    && order.Content.Description == content && order.Content.Weight == weight && order.Content.ContentType.Name == contentType.Name)
                {
                    MessageHelper.MessageNotChanges();
                    return false;
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }

        // Сотрудники 
        public static bool EmployeeLimitator(Employee? employee, string firstname, string lastname,
            string patronymic, DateOnly birthday, string phone, string passport, string login,
            string password, Post? post)
        {
            using var dbContext = new CourierServiceContext();

            // Проверка на пустые поля
            if (new[] { firstname, lastname, phone, passport, login, password }.Any(string.IsNullOrWhiteSpace))
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (post == null)
            {
                MessageHelper.MessageNullFields();
                return false;
            }
            else if (birthday == DateOnly.MinValue)
            {
                MessageHelper.MessageNullDate();
                return false;
            }
            else if (!Validations.ValidateCorrectAge(birthday))
            {
                MessageHelper.MessageInappropriateAge();
                return false;
            }
            else if (firstname.Length <= 2 || firstname.Length <= 2 || (patronymic != string.Empty && patronymic.Length <= 2))
            {
                MessageHelper.MessageShortFio();
                return false;
            }
            else if (phone.Length != 11)
            {
                MessageHelper.MessageShortPhone();
                return false;
            }
            else if (passport.Length != 10)
            {
                MessageHelper.MessageShortPassport();
                return false;
            }
            // Проверка на дубликаты и внесение изменений
            else if (employee != null)
            {
                if (dbContext.Employees.Any(r => r.Passport == passport && r.EmployeeId != employee.EmployeeId))
                {
                    MessageHelper.MessageDuplicatePassport();
                    return false;
                }
                else if (dbContext.Employees.Any(r => r.Phone == phone && r.EmployeeId != employee.EmployeeId))
                {
                    MessageHelper.MessageDuplicatePhone();
                    return false;
                }
                else if (dbContext.Employees.Any(r => r.Login == login && r.EmployeeId != employee.EmployeeId))
                {
                    MessageHelper.MessageDuplicateLogin();
                    return false;
                }

                else if (employee.Firstname == firstname && employee.Lastname == lastname && employee.Patronymic == patronymic
                    && employee.Birthday == birthday && employee.Passport == passport && employee.Phone == phone
                    && employee.Login == login && employee.Password == password && employee.PostId == post.PostId)
                {
                    MessageHelper.MessageNotChanges();
                    return false;
                }
            }
            // Добавление
            else
            {
                if (dbContext.Employees.Any(r => r.Passport == passport))
                {
                    MessageHelper.MessageDuplicatePassport();
                    return false;
                }
                else if (dbContext.Employees.Any(r => r.Phone == phone))
                {
                    MessageHelper.MessageDuplicatePhone();
                    return false;
                }
                else if (dbContext.Employees.Any(r => r.Login == login))
                {
                    MessageHelper.MessageDuplicateLogin();
                    return false;
                }
            }

            // Если ошибок нет, то возвращаем true
            return true;
        }
    }
}
