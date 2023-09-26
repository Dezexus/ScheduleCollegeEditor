using iTextSharp.text;
using iTextSharp.text.pdf;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ScheduleCollegeEditor.Classes.Export
{
    class ExportGroupSchedule
    {

        #region ExportInExcel

        /// <summary>
        /// Создаёт excel файл и запускает процесс экспорта
        /// </summary>
        public static async void ExportInExcel(List<Group> groups) {
            
            // Creating an instance of the app
            var excelApp = new Excel.Application();

            // Creating an Excel workbook
            Excel.Workbook workBook;

            var schedule = await DBConnection.GetScheduleAsync();
            schedule = schedule.Where(x => groups.Any(y => x._Group.Id == y.Id)).ToList();
            var shifts = schedule.Select(x => x.Shift).Distinct().ToList();

            foreach (var shift in shifts) {

                // Creating an Excel sheet
                Excel.Worksheet Sheet;
                workBook = excelApp.Workbooks.Add();
                Sheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                Sheet.Name = $"Смена №{shift}";
                var groups2 = groups.Where(x => schedule.Where(
                    y => y.Shift == shift).Any(z => z._Group.Id == x.Id)).ToList();
                FillWeeksForGroups(groups2, Sheet, excelApp);
            }
        }

        /// <summary>
        /// Заполняет excel расписанием занятий
        /// </summary>
        private static async void FillWeeksForGroups(List<Group> groups, Excel.Worksheet Sheet, Excel.Application excelApp) {

            int weekDayNumber = 0;
            int row = 1;
            int rowMax = 1;
            int colum = 1;

            foreach (var group in groups) {

                Sheet.Cells[row, colum + 1] = group.Name;
                weekDayNumber = 0;
                row++;

                foreach (var item in await GetWeekForGroup(group)) {

                    Sheet.Cells[row, colum] = item.FirstLesson.Number.ToString().Replace("0", "");
                    Sheet.Cells[row, colum + 1] = item.FirstLesson.SchoolSubject.Name + "\n" + item.FirstLesson.Teacher.FullName;
                    Sheet.Cells[row, colum + 2] = item.FirstLesson.Cabinet.Name;
                    Sheet.Cells[row, colum].RowHeight = 30;
                    row++;
                    Sheet.Cells[row, colum] = item.SecondLesson.Number.ToString().Replace("0", "");
                    Sheet.Cells[row, colum + 1] = item.SecondLesson.SchoolSubject.Name + "\n" + item.SecondLesson.Teacher.FullName;
                    Sheet.Cells[row, colum + 2] = item.SecondLesson.Cabinet.Name;
                    Sheet.Cells[row, colum].RowHeight = 30;
                    row++;
                    Sheet.Cells[row, colum] = item.ThirdLesson.Number.ToString().Replace("0", "");
                    Sheet.Cells[row, colum + 1] = item.ThirdLesson.SchoolSubject.Name + "\n" + item.ThirdLesson.Teacher.FullName;
                    Sheet.Cells[row, colum + 2] = item.ThirdLesson.Cabinet.Name;
                    Sheet.Cells[row, colum].RowHeight = 30;
                    row++;
                    Sheet.Cells[row, colum] = item.FourthLesson.Number.ToString().Replace("0", "");
                    Sheet.Cells[row, colum + 1] = item.FourthLesson.SchoolSubject.Name + "\n" + item.FourthLesson.Teacher.FullName;
                    Sheet.Cells[row, colum + 2] = item.FourthLesson.Cabinet.Name;
                    Sheet.Cells[row, colum].RowHeight = 30;
                    Sheet.Cells[row + 1, colum].RowHeight = 15;
                    Sheet.Cells[row + 1, colum + 1] = GlobalValue.WeekDays[weekDayNumber];
                    weekDayNumber++;
                    row += 2;
                }

                ChangingAppearanceWeek(Sheet, colum, row);
                colum += 4;
                rowMax = row > rowMax ? row : rowMax;
                row = 1;
            }

            ChangingAppearanceSheet(Sheet, colum, rowMax);
            //Opening the created excel file
            excelApp.Visible = true;
            excelApp.UserControl = true;

        }

        /// <summary>
        /// Осуществляет форматирование всей таблицы
        /// </summary>
        private static void ChangingAppearanceWeek(Excel.Worksheet Sheet, int colum, int row) {

            Sheet.Cells[1, colum].ColumnWidth = 6;
            Sheet.Cells[1, colum + 1].ColumnWidth = 17;
            Sheet.Cells[1, colum + 2].ColumnWidth = 10;
            Sheet.Cells[1, colum + 3].ColumnWidth = 1;

            string startRange1 = ExcelFunc.GetColumnLetter(colum) + 1; //Get the end range
            string endRange1 = ExcelFunc.GetColumnLetter(colum + 2) + (row - 1); //Get the end range

            Excel.Range cellsRange1 = Sheet.get_Range(startRange1, endRange1).Cells;

            cellsRange1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            cellsRange1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange1.Font.Name = "Times New Roman";
            Sheet.Cells[1, 1].RowHeight = 20;
            Sheet.Cells[1, 1].EntireRow.Font.Bold = true;
        }

        /// <summary>
        /// Формирует расписание занятости кабинета на неделю
        /// </summary>
        private static void ChangingAppearanceSheet(Excel.Worksheet Sheet, int colum, int rowMax) {

            Sheet.Cells[1, 1].EntireRow.Font.Bold = true; //Make group names bold

            string endRange2 = ExcelFunc.GetColumnLetter(colum) + rowMax; //Get the end range

            Excel.Range cellsRange2 = Sheet.get_Range("A1", endRange2).Cells;
            cellsRange2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange2.Font.Size = 11;
            cellsRange2.Font.Name = "Times New Roman";
        }

        #endregion

        #region ExportInPdf

        private static readonly BaseFont BaseFont = BaseFont.CreateFont("font/YandexSansText-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        private static readonly Font BaseFontPdf = new Font(BaseFont, 8);

        /// <summary>
        /// Создание pdf файла и его настройка
        /// </summary>
        public static async void ExportInPDF(List<Group> groups) {

            var file = new FileStream("pdf/ScheduleGroups.pdf", FileMode.Create);

            var document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
            var writer = PdfWriter.GetInstance(document, file);
            document.Open();
            document = await CreateStructurePdf(document, groups);
            document.Close();
            writer.Close();
            file.Close();
            Process.Start(Directory.GetCurrentDirectory() + "/pdf/ScheduleGroups.pdf");
        }

        /// <summary>
        /// Формирование структуры файла и его заполнение
        /// </summary>
        private static async Task<Document> CreateStructurePdf(Document document, List<Group> groups) {

            var tables = new List<PdfPTable>();

            foreach (var group in groups) {

                var temp = await CreateScheduleTableForGroup(group);
                if (temp == null) continue;
                tables.Add(temp);
            }

            var tempTableList = new List<PdfPTable>();
            var remainingTables = new List<PdfPTable>(tables); //List of tables that remained after the export of 5 pieces

            foreach (var table in tables) {

                tempTableList.Add(table);

                if (tempTableList.Count == 5) { //5 - the number of pages that fit on one page

                    document.Add(CreateExternalTable(tempTableList));
                    remainingTables.RemoveAll(x => tempTableList.Contains(x)); //Deletes all tables already exported from the list
                    tempTableList.Clear();
                    if (remainingTables.Count < 5) break;
                }
            }

            if (remainingTables.Count != 0)
                document.Add(CreateExternalTable(remainingTables)); //Export the remaining tables

            return document;
        }

        /// <summary>
        /// Формирует таблицы с расписанием на неделю
        /// </summary>
        public static async Task<PdfPTable> CreateScheduleTableForGroup(Group group) {

            var table = new PdfPTable(3);
            table.TotalWidth = 135f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 15f, 80f, 40f });

            table.AddCell(CreateCellAsTitle(group.Name));

            var week = await GetWeekForGroup(group);

            if (week.Count == 0) return null;

            foreach (var weekDay in week) {

                table.AddCell(CreateCellAsSeparator());
                table.AddCell(CreateBaseCell(weekDay.FirstLesson.Number.ToString().Replace("0", "")));
                table.AddCell(CreateBaseCell($"{weekDay.FirstLesson.SchoolSubject.Name}\n{weekDay.FirstLesson.Teacher.FullName}"));
                table.AddCell(CreateBaseCell(weekDay.FirstLesson.Cabinet.Name));

                table.AddCell(CreateBaseCell(weekDay.SecondLesson.Number.ToString().Replace("0", "")));
                table.AddCell(CreateBaseCell($"{weekDay.SecondLesson.SchoolSubject.Name}\n{weekDay.SecondLesson.Teacher.FullName}"));
                table.AddCell(CreateBaseCell(weekDay.SecondLesson.Cabinet.Name));

                table.AddCell(CreateBaseCell(weekDay.ThirdLesson.Number.ToString().Replace("0", "")));
                table.AddCell(CreateBaseCell($"{weekDay.ThirdLesson.SchoolSubject.Name}\n{weekDay.ThirdLesson.Teacher.FullName}"));
                table.AddCell(CreateBaseCell(weekDay.ThirdLesson.Cabinet.Name));

                table.AddCell(CreateBaseCell(weekDay.FourthLesson.Number.ToString().Replace("0", "")));
                table.AddCell(CreateBaseCell($"{weekDay.FourthLesson.SchoolSubject.Name}\n{weekDay.FourthLesson.Teacher.FullName}"));
                table.AddCell(CreateBaseCell(weekDay.FourthLesson.Cabinet.Name));
            }

            return table;
        }

        /// <summary>
        /// Создание внешней таблицы, в которой будут распологаться таблицы с расписанием
        /// </summary>
        private static PdfPTable CreateExternalTable(List<PdfPTable> tables) {

            var table = new PdfPTable(tables.Count);
            table.TotalWidth = 140f * tables.Count;
            table.LockedWidth = true;
            float[] widths = new float[tables.Count];
            for (int i = 0; i < widths.Length; i++)
                widths[i] = 140f;

            table.SetWidths(widths);
            tables.ForEach(x => table.AddCell(CreateCellNoBorder(x)));

            return table;
        }

        /// <summary>
        /// Создание ячейки-заголовка
        /// </summary>
        private static PdfPCell CreateCellAsTitle(string str) {

            var cell = new PdfPCell(new Phrase(str, BaseFontPdf));
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            return cell;
        }

        /// <summary>
        /// Создание разделяющей ячейки
        /// </summary>
        private static PdfPCell CreateCellAsSeparator() {

            var cell = new PdfPCell();
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            cell.FixedHeight = 2f;
            return cell;
        }

        /// <summary>
        /// Создание ячейки таблицы
        /// </summary>
        private static PdfPCell CreateBaseCell(string str) {

            var cell = new PdfPCell(new Phrase(str, BaseFontPdf));
            cell.HorizontalAlignment = 1;
            cell.FixedHeight = 20f;
            return cell;
        }

        /// <summary>
        /// Создание ячейки без границ
        /// </summary>
        private static PdfPCell CreateCellNoBorder(PdfPTable obj) {

            var cell = new PdfPCell();
            cell.HorizontalAlignment = 1;
            cell.AddElement(obj);
            cell.Border = 0;
            return cell;
        }

        #endregion

        #region General

        /// <summary>
        /// Формирует расписание занятий группы на неделю
        /// </summary>
        private static async Task<List<WeekForGroup>> GetWeekForGroup(Group group) {

            var weekDay = new List<WeekForGroup>();
            var groupSchedule = await DBConnection.GetScheduleForGroupByIdAsync(group.Id);

            foreach (var day in GlobalValue.WeekDays) {

                if (!groupSchedule.Any(x => x._WeekDay.DayName.Contains(day))) { //Creating an empty day of the week 
                                                                         //if the current day is missing from the group's schedule
                    weekDay.Add(new WeekForGroup());
                    continue;
                }
                var list = groupSchedule.Where(x => x._WeekDay.DayName == day).OrderBy(x => x.Number).ToList();

                weekDay.Add(new WeekForGroup {

                    FirstLesson = list.Count >= 1 ? list[0] : new Schedule(),
                    SecondLesson = list.Count >= 2 ? list[1] : new Schedule(),
                    ThirdLesson = list.Count >= 3 ? list[2] : new Schedule(),
                    FourthLesson = list.Count >= 4 ? list[3] : new Schedule(),
                });
            }
            return weekDay;
        }

        #endregion

    }

}
