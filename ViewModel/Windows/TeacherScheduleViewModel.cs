using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleCollegeEditor.ViewModel
{

    class TeacherScheduleViewModel : BaseViewModel
    {

        public TeacherScheduleViewModel(Teacher teacher) {

            Teacher = teacher;
            InicilizationCollection();
        }

        #region Field

        private List<string> daysWeek = new List<string>() {

            "Понедельник", "Вторник", "Среда",
            "Четверг", "Пятница", "Суббота"
        };
        private List<Schedule> schedule = new List<Schedule>();
        private Teacher teacher;

        #endregion

        #region Property

        public ObservableCollection<TeacherLessonCard> MondaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();
        public ObservableCollection<TeacherLessonCard> TuesdaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();
        public ObservableCollection<TeacherLessonCard> WednesdaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();
        public ObservableCollection<TeacherLessonCard> ThursdaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();
        public ObservableCollection<TeacherLessonCard> FridaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();
        public ObservableCollection<TeacherLessonCard> SaturdaySchedule { get; private set; }
            = new ObservableCollection<TeacherLessonCard>();

        public Teacher Teacher
        {
            get => teacher;
            set => Set(ref teacher, value);
        }

        #endregion

        #region Methods

        private async void InicilizationCollection() {

            schedule = await DBConnection.GetScheduleAsync();

            foreach (var day in daysWeek) {

                var teacherDaSchedule = CreateTeacherDaySchedule(day);

                switch (day) {

                    case "Понедельник":
                        teacherDaSchedule.ForEach(x => MondaySchedule.Add(x));
                        break;
                    case "Вторник":
                        teacherDaSchedule.ForEach(x => TuesdaySchedule.Add(x));
                        break;
                    case "Среда":
                        teacherDaSchedule.ForEach(x => WednesdaySchedule.Add(x));
                        break;
                    case "Четверг":
                        teacherDaSchedule.ForEach(x => ThursdaySchedule.Add(x));
                        break;
                    case "Пятница":
                        teacherDaSchedule.ForEach(x => FridaySchedule.Add(x));
                        break;
                    case "Суббота":
                        teacherDaSchedule.ForEach(x => SaturdaySchedule.Add(x));
                        break;
                }
            }
        }

        private List<TeacherLessonCard> CreateTeacherDaySchedule(string dayWeek) {

            var daySchedule = schedule.Where(x => x.Teacher.Id == teacher.Id
                && x._WeekDay.DayName == dayWeek).OrderBy(x => x.Number).ToList();

            var teacherDaSchedule = new List<TeacherLessonCard>();
            daySchedule.ForEach(x => teacherDaSchedule.Add(new TeacherLessonCard(x)));

            return teacherDaSchedule;
        }

        #endregion  

    }
}