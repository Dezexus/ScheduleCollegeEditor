using ScheduleCollegeEditor.Model;
using System.Collections.Generic;

namespace ScheduleCollegeEditor.ViewModel
{
    class TimeLessonViewModel : BaseViewModel
    {
        public static List<TimeLessonViewModel> linkViewModels { get; set; } 
            = new List<TimeLessonViewModel>();

        public TimeLessonViewModel(Time time = null, string dayWeek = null) {

            if (dayWeek != null) timeLesson.WeekDayNumber = Time.GetWeekDayNumberByDayName(dayWeek);
            if (time != null) TimeLesson = time;
            linkViewModels.Add(this);
        }

        #region Field

        private Time timeLesson = new Time();
        private bool toDelete = false;

        #endregion

        #region Property

        public Time TimeLesson {

            get => timeLesson;
            set => Set(ref timeLesson, value);
        }

        public bool ToDelete {

            get => toDelete;
            set => Set(ref toDelete, value);
        }

        #endregion

    }
}