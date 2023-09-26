using ScheduleCollegeEditor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ScheduleCollegeEditor.Classes.Export
{
    class ExportManHoursGroupByLesson
    {

        #region ExportInExcel

        /// <summary>
        /// Создаёт excel файл и запускает процесс экспорта
        /// </summary>
        public static void ExportInExcel(string mode = "Week") {

            // Creating an instance of the app
            var excelApp = new Excel.Application();

            // Creating an Excel workbook
            Excel.Workbook workBook;

            // Creating an Excel sheet
            Excel.Worksheet Sheet;
            workBook = excelApp.Workbooks.Add();
            Sheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
            Sheet.Name = "Часы занятий";

            if (mode == "Week") {

                FillTeacherInWeek(Sheet, excelApp);
            } else {

                FillTeacherForCurrentDay(Sheet, excelApp);
            }
        }

        /// <summary>
        /// Заполняет excel статистикой количесва часов по кажому предмета преподавателя за неделю
        /// </summary>
        private static async void FillTeacherInWeek(Excel.Worksheet Sheet, Excel.Application excelApp) {

            int row = 2;
            int rowMax = 1;
            int colum = 1;

            Sheet.Cells[1, colum] = "Преподаватель";
            Sheet.Cells[1, colum + 1] = "Предмет";
            Sheet.Cells[1, colum + 2] = "Часы";

            foreach (var numberTeacherHours in await GetWeekForTeacher()) {
                foreach (var item in numberTeacherHours.CountHourForItems) {

                    Sheet.Cells[row, colum] = numberTeacherHours.Teacher;
                    Sheet.Cells[row, colum + 1] = item.Name;
                    Sheet.Cells[row, colum + 2] = item.Hour;
                    Sheet.Cells[row, colum].RowHeight = 20;
                    row++;
                }
                Sheet.Cells[row, colum].RowHeight = 8;
                Sheet.Cells[row, colum].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                Sheet.Cells[row, colum + 1].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                Sheet.Cells[row, colum + 2].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                row++;
            }

            ChangingAppearanceWeek(Sheet, colum, row);
            rowMax = row > rowMax ? row : rowMax;

            ChangingAppearanceSheet(Sheet, colum, rowMax);
            //Opening the created excel file
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }

        /// <summary>
        /// Заполняет excel статистикой количесва часов по кажому предмета преподавателя за текущий день
        /// </summary>
        private static async void FillTeacherForCurrentDay(Excel.Worksheet Sheet, Excel.Application excelApp) {

            int row = 2;
            int rowMax = 1;
            int colum = 1;

            Sheet.Cells[1, colum] = "Преподаватель";
            Sheet.Cells[1, colum + 1] = "Предмет";
            Sheet.Cells[1, colum + 2] = "Часы";

            DateTime dateValue = DateTime.Now;
            var day = GlobalValue.WeekDays[(int)dateValue.DayOfWeek - 1];

            Sheet.Cells[row, colum + 1] = day;
            Sheet.Cells[row, colum + 1].Font.Bold = true;
            Sheet.Cells[row, colum].RowHeight = 30;
            row++;

            foreach (var numberTeacherHours in await GetDayForTeacher(day)) {
                foreach (var item in numberTeacherHours.CountHourForItems) {

                    Sheet.Cells[row, colum] = numberTeacherHours.Teacher;
                    Sheet.Cells[row, colum + 1] = item.Name;
                    Sheet.Cells[row, colum + 2] = item.Hour;
                    Sheet.Cells[row, colum].RowHeight = 20;
                    row++;
                }
                Sheet.Cells[row, colum].RowHeight = 8;
                Sheet.Cells[row, colum].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                Sheet.Cells[row, colum + 1].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                Sheet.Cells[row, colum + 2].Interior.Color = Excel.XlRgbColor.rgbLightGray;
                row++;
            }


            ChangingAppearanceWeek(Sheet, colum, row);
            rowMax = row > rowMax ? row : rowMax;

            ChangingAppearanceSheet(Sheet, colum, rowMax);
            //Opening the created excel file
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }

        /// <summary>
        /// Осуществляет форматирование недели
        /// </summary>
        private static void ChangingAppearanceWeek(Excel.Worksheet Sheet, int colum, int row) {

            Sheet.Cells[1, colum].ColumnWidth = 25;
            Sheet.Cells[1, colum + 1].ColumnWidth = 25;
            Sheet.Cells[1, colum + 2].ColumnWidth = 10;

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

        #endregion

        #region General

        /// <summary>
        /// Выдаёт статистику количесва часов по кажому предмету преподавателя за неделю
        /// </summary>
        private static async Task<List<NumberTeacherHours>> GetWeekForTeacher() {

            var numberTeacherHoursList = new List<NumberTeacherHours>();
            var schedules = await DBConnection.GetScheduleAsync();
            var teachers = schedules.OrderBy(x => x.Teacher.FullName).Select(x => x.Teacher.FullName).Distinct().ToList();
      
            foreach (var teacher in teachers) {

                var numberTeacherHours = new NumberTeacherHours();
                numberTeacherHours.Teacher = teacher;

                var schooleSubjects = schedules.Where(x => x.Teacher.FullName == teacher).OrderBy(x => 
                    x.SchoolSubject.Name).Select(x => x.SchoolSubject.Name).Distinct().ToList();

                foreach (var schooleSubject in schooleSubjects) {

                    int hours = schedules.Where(x => x.Teacher.FullName == teacher
                        && x.SchoolSubject.Name == schooleSubject).Count() * 2;

                    var item = new CountHourForItem {
                        Name = schooleSubject,
                        Hour = hours
                    };
                    numberTeacherHours.CountHourForItems.Add(item);
                }
                numberTeacherHoursList.Add(numberTeacherHours);
            }
            
            return numberTeacherHoursList;
        }

        /// <summary>
        /// Выдаёт статистику количесва часов по кажому предмету преподавателя за текущий день
        /// </summary>
        private static async Task<List<NumberTeacherHours>> GetDayForTeacher(string day) {

            var numberTeacherHoursList = new List<NumberTeacherHours>();
            var schedules = await DBConnection.GetScheduleAsync();
            var teachers = schedules.Where(x => x._WeekDay.DayName == day).OrderBy(x => x.Teacher.FullName).Select(
                x => x.Teacher.FullName).Distinct().ToList();

            foreach (var teacher in teachers) {

                var numberTeacherHours = new NumberTeacherHours();
                numberTeacherHours.Teacher = teacher;

                var schooleSubjects = schedules.Where(x => x._WeekDay.DayName == day && x.Teacher.FullName == teacher).OrderBy(
                    x => x.SchoolSubject.Name).Select(x => x.SchoolSubject.Name).Distinct().ToList();

                foreach (var schooleSubject in schooleSubjects) {

                    int hours = schedules.Where(x => x._WeekDay.DayName == day && x.Teacher.FullName == teacher 
                        && x.SchoolSubject.Name == schooleSubject).Count() * 2;

                    var item = new CountHourForItem {
                        Name = schooleSubject,
                        Hour = hours
                    };
                    numberTeacherHours.CountHourForItems.Add(item);
                }
                numberTeacherHoursList.Add(numberTeacherHours);
            }

            return numberTeacherHoursList;
        }

        #endregion

    }

}
