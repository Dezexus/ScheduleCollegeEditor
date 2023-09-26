using ScheduleCollegeEditor.Model;

namespace ScheduleCollegeEditor.ViewModel
{
    class CabinetLessonCardViewModel : BaseViewModel
    {
        public CabinetLessonCardViewModel(Schedule schedule) {

            Lesson = schedule;
        }

        #region Field

        private Schedule lesson;

        #endregion

        #region Property

        public Schedule Lesson {
            get => lesson;
            set => Set(ref lesson, value);
        }

        #endregion

    }
}