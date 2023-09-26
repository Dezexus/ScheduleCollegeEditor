using ScheduleCollegeEditor.Classes.Export;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;

namespace ScheduleCollegeEditor.Classes
{
    /// <summary>
    /// Отвечает за экспорт данных в различных форматах
    /// </summary>
    class Report {

        
        public static void ExportGroupScheduleInExcel(List<Group> groups) {

            ExportGroupSchedule.ExportInExcel(groups);
        }

        public static void ExportGroupScheduleInPDF(List<Group> groups) {

            ExportGroupSchedule.ExportInPDF(groups);
        }

        public static void ExportCabinetScheduleInExcel(List<ClassRoom> cabinets) {

            ExportCabinetScheduleClass.ExportInExcel(cabinets);
        }

        public static void ExportTeacherScheduleInExcel(List<Teacher> teachers) {

            ExportTeacherScheduleClass.ExportInExcel(teachers);
        }

        public static void ExportManHoursGroupByLessonInExcel(string mode = "Week") {

            ExportManHoursGroupByLesson.ExportInExcel(mode);
        }

        public static void ExportManHoursGroupByLessonAndGroupInExcel(string mode = "Week") {

            ExportManHoursGroupByLessonAndGroup.ExportInExcel(mode);
        }

    }
}

