using ScheduleCollegeEditor.Model;

namespace ScheduleCollegeEditor.ViewModel
{
    class TeacherLessonCardViewModel : BaseViewModel
    {
        public TeacherLessonCardViewModel(Schedule schedule) {

            Lesson = schedule;
        }

        #region Fields

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