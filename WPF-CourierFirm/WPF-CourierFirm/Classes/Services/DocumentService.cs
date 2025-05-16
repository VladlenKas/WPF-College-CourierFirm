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
        public static void GenerateReport(string outputPath, DateOnly startDate, DateOnly endDate, List<ReportData> deliveries, string adminName, string logoPath)
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

                // 1. Заголовок и логотип
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                headerTable.LockedWidth = true;

                // Ячейка с заголовком
                PdfPCell headerCell = new PdfPCell(new Phrase("Отчет", headerFont));
                headerCell.Border = Rectangle.NO_BORDER;
                headerCell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerCell.PaddingBottom = 10f; // отступ снизу для заголовка - 10 пунктов
                headerTable.AddCell(headerCell);

                if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);

                    PdfPCell logoCell = new PdfPCell(logo, true);
                    logoCell.Border = Rectangle.NO_BORDER;
                    logoCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    logoCell.PaddingBottom = 10f; // отступ снизу для логотипа
                    headerTable.AddCell(logoCell);
                }
                else
                {
                    PdfPCell emptyCell = new PdfPCell(new Phrase(" "));
                    emptyCell.Border = Rectangle.NO_BORDER;
                    emptyCell.PaddingBottom = 10f; // отступ снизу
                    headerTable.AddCell(emptyCell);
                }

                document.Add(headerTable);

                // 2. Период отчета
                Paragraph periodParagraph = new Paragraph(
                    $"Период по доставкам с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}",
                    periodFont);
                periodParagraph.SpacingBefore = 10f; // отступ сверху 10 пунктов
                periodParagraph.SpacingAfter = 10f;  // отступ снизу 10 пунктов
                periodParagraph.Leading = periodFont.Size * 1.5f;  // отступ снизу 10 пунктов
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
                    deliveriesHeader.Leading = normalFont.Size * 1.5f;  // отступ снизу 10 пунктов
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
                    noDeliveries.Leading = normalFont.Size * 1.5f;  // отступ снизу 10 пунктов
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
                summary.Leading = periodFont.Size * 1.5f;  // отступ снизу 10 пунктов
                document.Add(summary);

                // 5. Дата составления
                Paragraph reportDate = new Paragraph(
                    $"Дата составления отчета: {DateTime.Now:dd.MM.yyyy}",
                    normalFont)
                {
                    SpacingAfter = 5f // отступ снизу 5 пунктов
                };
                reportDate.Leading = normalFont.Size * 1.5f;  // отступ снизу 10 пунктов
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
