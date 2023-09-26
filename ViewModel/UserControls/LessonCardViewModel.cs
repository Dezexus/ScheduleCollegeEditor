using ScheduleCollegeEditor.Model;
using System.Windows.Media;

namespace ScheduleCollegeEditor.ViewModel
{
    class LessonCardViewModel : BaseViewModel
    {
        public LessonCardViewModel(Schedule schedule) {

            Lesson = schedule;
            SelectBacgroundColor(schedule);
        }

        #region Field

        private Schedule lesson;
        private Brush bacground;

        #endregion

        #region Property

        public Schedule Lesson {

            get => lesson;
            set => Set(ref lesson, value);
        }

        public Brush Background {

            get => bacground;
            set => Set(ref bacground, value);
        }

        #endregion

        #region Method

        private void SelectBacgroundColor(Schedule schedule) {

            string hexCode = "#c96363";
            if (schedule.Changed) hexCode = "#ffda91";

            Background = (Brush)(new BrushConverter().ConvertFrom(hexCode));
        }

        #endregion

    }
}