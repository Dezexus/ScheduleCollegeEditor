using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Collections.Generic;

namespace ScheduleCollegeEditor.Data
{
    class GlobalValue
    {

        #region List

        public static List<string> WeekDays { get; } = new List<string>() {
            "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

        private static List<Time> _Times = new List<Time>();
        private static List<ClassRoom> _Cabinets = new List<ClassRoom>();
        private static List<Class> _GroupSchoolSubjects = new List<Class>();
        private static List<Teacher> _Teachers = new List<Teacher>();
        private static List<Schedule> _Schedules = new List<Schedule>();
        private static List<Group> _Groups = new List<Group>();

        public static List<Time> Times {
            get => _Times;
            set {
                _Times.Clear();
                _Times = value;
            }

        }

        public static List<ClassRoom> Cabinets {
            get => _Cabinets;
            set {
                _Cabinets.Clear();
                _Cabinets = value;
            }

        }

        public static List<Class> GroupSchoolSubjects {
            get => _GroupSchoolSubjects;
            set {
                _GroupSchoolSubjects.Clear();
                _GroupSchoolSubjects = value;
            }

        }

        public static List<Teacher> Teachers {
            get => _Teachers;
            set {
                _Teachers.Clear();
                _Teachers = value;
            }

        }

        public static List<Group> Groups {
            get => _Groups;
            set {
                _Groups.Clear();
                _Groups = value;
            }

        }

        public static List<Schedule> Schedules {
            get => _Schedules;
            set {
                _Schedules = new List<Schedule>(value);
            }

        }

        #endregion

        public static FillGroupScheduleViewModel FillGroupScheduleViewModelRef { get; set; }

        public static byte Shift { get; set; }

        public static int NumberLessons { get; set; }
    }
}