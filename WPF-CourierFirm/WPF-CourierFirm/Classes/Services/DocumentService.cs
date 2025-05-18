using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CourierFrim.Model;
using WPF_CourierFrim.Classes.Helpers;
using ControlzEx.Standard;

namespace WPF_CourierFrim.Classes.Services
{
    public static class DocumentService
    {
        public static void GenerateReport(string outputPath, DateOnly startDate, DateOnly endDate, List<ReportData> deliveries, string adminName)
        {
            // Создаем документ с указанными отступами
            Document document = new Document(PageSize.A4, 85f, 42f, 57f, 57f);

            try
            {
                // Создаем PDF writer и открываем документ
                PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
                document.Open();

                // Регистрируем провайдер кодировок(обязательно!)
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                // Шрифты (Times New Roman - стандарт для РФ)
                BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont boldFont = BaseFont.CreateFont("c:\\windows\\fonts\\timesbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                Font headerFont = new Font(boldFont, 16f, Font.NORMAL);
                Font periodFont = new Font(boldFont, 14f, Font.NORMAL);
                Font normalFont = new Font(baseFont, 14f, Font.NORMAL);
                Font tableHeaderFont = new Font(boldFont, 12f, Font.NORMAL);
                Font tableCellFont = new Font(baseFont, 12f, Font.NORMAL);

                // 1. Заголовок
                Paragraph titleParagraph = new Paragraph(
                    "Отчет",
                    headerFont);
                titleParagraph.SpacingAfter = 10f;  // отступ снизу 10 пунктов
                titleParagraph.Leading = periodFont.Size * 1.5f;
                document.Add(titleParagraph);

                // 2. Период отчета
                Paragraph periodParagraph = new Paragraph(
                    $"Период по доставкам с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}",
                    periodFont);
                periodParagraph.SpacingBefore = 10f; // отступ сверху 10 пунктов
                periodParagraph.SpacingAfter = 10f;  // отступ снизу 10 пунктов
                periodParagraph.Leading = periodFont.Size * 1.5f; 
                document.Add(periodParagraph);

                // 3. Таблица доставок
                if (deliveries.Count != 0) // Если доставки есть, создаем таблицу
                {
                    // Раздел "Доставки"
                    Paragraph deliveriesHeader = new Paragraph("Доставки:", normalFont)
                    {
                        SpacingBefore = 10f, // отступ сверху 10 пунктов
                        SpacingAfter = 5f    // отступ снизу 5 пунктов
                    };
                    deliveriesHeader.Leading = normalFont.Size * 1.5f;  
                    document.Add(deliveriesHeader);

                    PdfPTable deliveryTable = new PdfPTable(7);
                    deliveryTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                    deliveryTable.LockedWidth = true;

                    // Настройка ширины столбцов (в процентах от общей ширины)
                    float[] columnWidths = { 12f, 15f, 15f, 15f, 12f, 12f, 12f };
                    deliveryTable.SetWidths(columnWidths);

                    // Заголовки таблицы
                    string[] headers = { "№ доставки", "ФИО курьера", "ФИО клиента", "Организация", "Тариф", "Тип", "Способ оплаты" };
                    foreach (string header in headers)
                    {
                        PdfPCell headerCellTable = new PdfPCell(new Phrase(header, tableHeaderFont));
                        headerCellTable.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCellTable.VerticalAlignment = Element.ALIGN_MIDDLE;
                        headerCellTable.Padding = 5f;
                        deliveryTable.AddCell(headerCellTable);
                    }

                    // Вывод данных в таблицу
                    foreach (ReportData delivery in deliveries)
                    {
                        AddTableCell(deliveryTable, delivery.NumberDelivery, tableCellFont, Element.ALIGN_RIGHT);
                        AddTableCell(deliveryTable, delivery.FullnameCourier, tableCellFont);
                        AddTableCell(deliveryTable, delivery.FullnameClient, tableCellFont);
                        AddTableCell(deliveryTable, delivery.OrganisationName, tableCellFont);
                        AddTableCell(deliveryTable, delivery.RateName, tableCellFont);
                        AddTableCell(deliveryTable, delivery.ContentTypeName, tableCellFont);
                        AddTableCell(deliveryTable, delivery.PaymentMethod, tableCellFont);
                    }

                    deliveryTable.SpacingBefore = 5f; // отступ сверху 5 пунктов перед таблицей
                    deliveryTable.SpacingAfter = 10f; // отступ снизу 10 пунктов после таблицы

                    document.Add(deliveryTable); 
                }
                else // Либо пишем, что доставок нет
                {
                    Paragraph noDeliveries = new Paragraph(
                        $"Доставки за этот период отсутствуют",
                        normalFont)
                    {
                        SpacingAfter = 5f // отступ снизу 5 пунктов
                    };
                    noDeliveries.Leading = normalFont.Size * 1.5f;  
                    document.Add(noDeliveries);
                }

                // 4. Итоговая строка
                Paragraph summary = new Paragraph(
                    $"Итого: {deliveries.Count} доставок(-ки)",
                    periodFont)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 15f, // отступ сверху 15 пунктов для отделения от таблицы
                    SpacingAfter = 15f   // отступ снизу 15 пунктов
                };
                summary.Leading = periodFont.Size * 1.5f;  
                document.Add(summary);

                // 5. Дата составления
                Paragraph reportDate = new Paragraph(
                    $"Дата составления отчета: {DateTime.Now:dd.MM.yyyy}",
                    normalFont)
                {
                    SpacingAfter = 5f // отступ снизу 5 пунктов
                };
                reportDate.Leading = normalFont.Size * 1.5f; 
                document.Add(reportDate);

                // 6. ФИО администратора
                Paragraph adminParagraph = new Paragraph(
                    $"ФИО администратора: {adminName}",
                    normalFont);
                // После последнего параграфа отступы не нужны 
                adminParagraph.Leading = normalFont.Size * 1.5f;  // отступ снизу 10 пунктов
                document.Add(adminParagraph);
            }
            finally
            {
                document.Close();
            }
        }

        public static void GenerateReceipt(string outputPath, Delivery delivery)
        {
            // Создаем документ с указанными отступами
            Document document = new Document(PageSize.A4, 85f, 42f, 57f, 57f);

            try
            {
                // Создаем PDF writer и открываем документ
                PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
                document.Open();

                // Регистрируем провайдер кодировок(обязательно!)
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                // Шрифты (Times New Roman - стандарт для РФ)
                BaseFont baseFont = BaseFont.CreateFont("c:\\windows\\fonts\\times.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont boldFont = BaseFont.CreateFont("c:\\windows\\fonts\\timesbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                Font headerFont = new Font(boldFont, 16f, Font.NORMAL);
                Font periodFont = new Font(boldFont, 14f, Font.NORMAL);
                Font normalFont = new Font(baseFont, 14f, Font.NORMAL);
                Font tableHeaderFont = new Font(boldFont, 12f, Font.NORMAL);
                Font tableCellFont = new Font(baseFont, 12f, Font.NORMAL);

                // 1. Заголовок
                Paragraph titleParagraph = new Paragraph(
                    "Чек об оказании услуг доставки",
                    headerFont);
                titleParagraph.SpacingAfter = 10f;  // отступ снизу 10 пунктов
                titleParagraph.Leading = periodFont.Size * 1.5f;
                document.Add(titleParagraph);

                // 2. Данные отправителей, получателей
                Paragraph organisationName = new Paragraph(
                    $"Отправитель (наименование организации): {delivery.Order.Organisation.Name}",
                    normalFont);
                organisationName.SpacingBefore = 5f;
                organisationName.Leading = normalFont.Size * 1.5f;
                document.Add(organisationName);

                Paragraph clientFullname = new Paragraph(
                    $"Получатель (ФИО клиента): {delivery.Order.FullnameClient}",
                    normalFont);
                clientFullname.SpacingBefore = 5f;
                clientFullname.Leading = normalFont.Size * 1.5f;
                document.Add(clientFullname);

                Paragraph courierFulname = new Paragraph(
                    $"Курьер (ФИО сотрудника): {delivery.EmployeeDeliveries.First().Employee.Fullname}",
                    normalFont);
                courierFulname.SpacingBefore = 5f;
                courierFulname.Leading = normalFont.Size * 1.5f;
                document.Add(courierFulname);

                // 3. Данные адресов
                Paragraph receivingAddress = new Paragraph(
                    $"Адрес отправителя: {delivery.Order.ReceivingAddress}",
                    normalFont);
                receivingAddress.SpacingBefore = 5f;
                receivingAddress.Leading = normalFont.Size * 1.5f;
                document.Add(receivingAddress);

                Paragraph deliveryAddress = new Paragraph(
                    $"Адрес получателя: {delivery.Order.DeliveryAddress}",
                    normalFont);
                deliveryAddress.SpacingBefore = 5f;
                deliveryAddress.Leading = normalFont.Size * 1.5f;
                document.Add(deliveryAddress);

                // 4. Данные о времени
                Paragraph startingDate = new Paragraph(
                    $"Дата и время создания заказа: {delivery.Order.DatetimeCreation}",
                    normalFont);
                startingDate.SpacingBefore = 5f;
                startingDate.Leading = normalFont.Size * 1.5f;
                document.Add(startingDate);

                Paragraph endingDate = new Paragraph(
                    $"Дата и время завершения доставки: {delivery.DatetimePresentation}",
                    normalFont);
                endingDate.SpacingBefore = 5f;
                endingDate.Leading = normalFont.Size * 1.5f;
                document.Add(endingDate);

                // Раздел "Доставки"
                Paragraph deliveriesHeader = new Paragraph("Доставки:", normalFont)
                {
                    SpacingBefore = 10f, // отступ сверху 10 пунктов
                    SpacingAfter = 5f    // отступ снизу 5 пунктов
                };
                deliveriesHeader.Leading = normalFont.Size * 1.5f;
                document.Add(deliveriesHeader);

                PdfPTable deliveryTable = new PdfPTable(7);
                deliveryTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                deliveryTable.LockedWidth = true;

                // Настройка ширины столбцов (в процентах от общей ширины)
                float[] columnWidths = { 12f, 12f, 15f, 10f, 15f, 13f, 18f };
                deliveryTable.SetWidths(columnWidths);

                // Заголовки таблицы
                string[] headers = { "№ доставки", "Тариф", "Стоимость тарифа", "Вес", "Тип", "Способ оплаты", "Содержимое заказа" };
                foreach (string header in headers)
                {
                    PdfPCell headerCellTable = new PdfPCell(new Phrase(header, tableHeaderFont));
                    headerCellTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCellTable.VerticalAlignment = Element.ALIGN_MIDDLE;
                    headerCellTable.Padding = 5f;
                    deliveryTable.AddCell(headerCellTable);
                }

                AddTableCell(deliveryTable, delivery.DeliveryId.ToString(), tableCellFont, Element.ALIGN_RIGHT);
                AddTableCell(deliveryTable, delivery.Order.Rate.Name, tableCellFont);
                AddTableCell(deliveryTable, $"{delivery.Order.Rate.Cost.ToString("N0")} руб.", tableCellFont, Element.ALIGN_RIGHT);
                AddTableCell(deliveryTable, $"{delivery.Order.Content.Weight.ToString()} кг", tableCellFont, Element.ALIGN_RIGHT);
                AddTableCell(deliveryTable, delivery.Order.Content.ContentType.Name, tableCellFont);
                AddTableCell(deliveryTable, TypeHelper.PaymentMethodToString(delivery.PaymentMethod), tableCellFont);
                AddTableCell(deliveryTable, delivery.Order.Content.Description, tableCellFont);

                deliveryTable.SpacingBefore = 5f; // отступ сверху 5 пунктов перед таблицей
                deliveryTable.SpacingAfter = 10f; // отступ снизу 10 пунктов после таблицы

                document.Add(deliveryTable);

                // 4. Итоговая строка
                Paragraph summary = new Paragraph(
                    $"Итого: {delivery.Order.Rate.Cost.ToString("N0")} руб. за доставку",
                    periodFont)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 15f, // отступ сверху 15 пунктов для отделения от таблицы
                };
                summary.Leading = periodFont.Size * 1.5f;
                document.Add(summary);
            }
            finally
            {
                document.Close();
            }
        }

        private static void AddTableCell(PdfPTable table, string text, Font font,
            int alignment = Element.ALIGN_LEFT)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Padding = 5f;
            cell.HorizontalAlignment = alignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(cell);
        }
    }
}
