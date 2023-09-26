using ScheduleCollegeEditor.Model;

namespace ScheduleCollegeEditor.ViewModel
{
    class InfoIntersectionLessonCardViewModel : BaseViewModel
    {
        public InfoIntersectionLessonCardViewModel(Schedule schedule) {

            Lesson = schedule;
        }

        #region Fields

        private Schedule lesson;

        #endregion

        #region Propertys

        public Schedule Lesson {

            get => lesson;
            set => Set(ref lesson, value);
        }

        #endregion

    }
}