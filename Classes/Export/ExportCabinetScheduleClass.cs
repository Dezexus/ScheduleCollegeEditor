﻿using ScheduleCollegeEditor.Classes.Export;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ScheduleCollegeEditor.Classes
{
    class ExportCabinetScheduleClass
    {

        /// <summary>
        /// Создаёт excel файл и запускает процесс экспорта
        /// </summary>
        public static void ExportInExcel(List<ClassRoom> cabinets) {

            // Creating an instance of the app
            Excel.Application excelApp = new Excel.Application();

            // Creating an Excel workbook
            Excel.Workbook workBook;

            // Creating an Excel sheet
            Excel.Worksheet Sheet;
            workBook = excelApp.Workbooks.Add();
            Sheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            Sheet.Name = "Расписание";

            FillWeeksForCabinets(cabinets, Sheet, excelApp);
        }

        /// <summary>
        /// Заполняет excel расписанием занятий
        /// </summary>
        private static async void FillWeeksForCabinets(List<ClassRoom> cabinets, Excel.Worksheet Sheet, Excel.Application excelApp) {

            int row = 1;
            int rowMax = 1;
            int colum = 1;

            foreach (var cabinet in cabinets) {

                Sheet.Cells[row, colum + 1] = cabinet.Name;
                row++;

                foreach (var scheduleOnDay in await GetWeekForCabinet(cabinet)) {

                    Sheet.Cells[row, colum + 1] = scheduleOnDay.WeekDay;
                    Sheet.Cells[row, colum + 1].Font.Bold = true;
                    Sheet.Cells[row, colum + 1].EntireRow.Interior.Color = Excel.XlRgbColor.rgbYellow;
                    row++;

                    foreach (var schedule in scheduleOnDay.scheduleList) {

                        Sheet.Cells[row, colum] = schedule.Number.ToString();
                        Sheet.Cells[row, colum + 1] =  schedule._Group.Name;
                        Sheet.Cells[row, colum].RowHeight = 25;
                        row++;
                    }
                }

                ChangingAppearanceWeek(Sheet, colum, row);
                colum += 3;
                rowMax = row > rowMax ? row : rowMax;
                row = 1;
            }

            ChangingAppearanceSheet(Sheet, colum, rowMax);
            //Opening the created excel file
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }

        /// <summary>
        /// Осуществляет форматирование расписания за неделю
        /// </summary>
        private static void ChangingAppearanceWeek(Excel.Worksheet Sheet, int colum, int row) {

            Sheet.Cells[1, colum].ColumnWidth = 6;
            Sheet.Cells[1, colum + 1].ColumnWidth = 17;
            Sheet.Cells[1, colum + 2].ColumnWidth = 1;

            string startRange1 = ExcelFunc.GetColumnLetter(colum) + 1; //Get the end range
            string endRange1 = ExcelFunc.GetColumnLetter(colum + 1) + (row - 1); //Get the end range

            Excel.Range cellsRange1 = Sheet.get_Range(startRange1, endRange1).Cells;

            cellsRange1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            cellsRange1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange1.Font.Name = "Times New Roman";
            Sheet.Cells[1, 1].RowHeight = 20;
            Sheet.Cells[1, 1].EntireRow.Font.Bold = true;
        }

        /// <summary>
        /// Осуществляет форматирование всей таблицы
        /// </summary>
        private static void ChangingAppearanceSheet(Excel.Worksheet Sheet, int colum, int rowMax) {

            Sheet.Cells[1, 1].EntireRow.Font.Bold = true; //Make group names bold

            string endRange2 = ExcelFunc.GetColumnLetter(colum) + rowMax; //Get the end range

            Excel.Range cellsRange2 = Sheet.get_Range("A1", endRange2).Cells;
            cellsRange2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            cellsRange2.Font.Size = 11;
            cellsRange2.Font.Name = "Times New Roman";
        }

        /// <summary>
        /// Формирует расписание занятости кабинета на неделю
        /// </summary>
        private static async Task<List<ScheduleOnDay>> GetWeekForCabinet(ClassRoom cabinet) {

            var scheduleOnWeek = new List<ScheduleOnDay>();
            var cabinetschedule = await DBConnection.GetCabinetScheduleByIdAsync(cabinet.Id);
            var allSchedule = await DBConnection.GetScheduleAsync();
            byte maxNumberPair = (byte)allSchedule.Max(x => x.Number);

            foreach (var day in GlobalValue.WeekDays) {

                var schedule = cabinetschedule.Where(x => x._WeekDay.DayName == day).ToList();
                var scheduleOnDay = new ScheduleOnDay { WeekDay = day};
                scheduleOnDay.SetSchedule(schedule, maxNumberPair);

                scheduleOnWeek.Add(scheduleOnDay);
            }
            return scheduleOnWeek;
        }

    }

}

