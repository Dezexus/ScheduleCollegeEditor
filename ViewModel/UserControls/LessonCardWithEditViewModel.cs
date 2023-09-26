using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    public class LessonCardWithEditViewModel : BaseViewModel
    {
        public static List<LessonCardWithEditViewModel> links 
            = new List<LessonCardWithEditViewModel>();

        public LessonCardWithEditViewModel(Schedule _lesson, string _weekDay, Group _group) {

            group = _group;
            _lesson = (Schedule)_lesson.Clone();
            links.Add(this);
            weekDay = _weekDay;
            LessonNotChanged = GetScheduleForSave(_lesson);
            Lesson = InicializationLesson(_lesson);
            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private Schedule lesson;
        private string weekDay;
        private Group group;

        #endregion

        #region Property

        public ObservableCollection<Class> GroupSchoolSubjectsList { get; private set; }
                                        = new ObservableCollection<Class>();
        public ObservableCollection<Teacher> TeachersList { get; private set; }
                                                = new ObservableCollection<Teacher>();
        public ObservableCollection<ClassRoom> CabinetsList { get; private set; }
                                        = new ObservableCollection<ClassRoom>();

        public ObservableCollection<Time> TimeList { get; private set; }
                                = new ObservableCollection<Time>();

        public Schedule Lesson {
            get => lesson;
            set => Set(ref lesson, value);
        }

        public ScheduleForSave LessonNotChanged { get; private set; }
        public bool LocalChangedStatus { get; private set; }
        public bool DeletedChangedStatus { get; private set; }
        public Schedule RestoreLessonObject { get; set; }

        #endregion

        #region Command

        public ICommand CheckIntersectionCommand { get; private set; }
        public ICommand RestoreLessonCommand { get; private set; }
        public ICommand SaveChangedLessonCommand { get; private set; }
        public ICommand RestoreChangedLessonCommand { get; private set; }

        #endregion

        #region CommandMethod

        private void CheckIntersection(object obj) {

            WeekDayScheduleEditorViewModel.CheckIntersection();
        }

        private void SaveChangedLesson(object numberLesson) {

            if (!LocalChangedStatus) {

                LocalChangedStatus = true;
                DeletedChangedStatus = false;
            }
        }

        private void RestoreChangedLesson(object numberLesson) {

            if (LocalChangedStatus) {

                Lesson = InicializationLesson(Schedule.ScheduleForSaveToSchedule(LessonNotChanged));
                LocalChangedStatus = false;
                DeletedChangedStatus = true;
                return;
            }

            if (LocalDBConnection.ExistChangedLesson(GetScheduleForSave())) {

                var _lesson = LocalDBConnection.GetChangedLesson(GetScheduleForSave());
                Lesson = InicializationLesson(Schedule.ScheduleForSaveToSchedule(_lesson));
                DeletedChangedStatus = true;
            }
        }

        private void RestoreLesson(object numberLesson) {

            var window = new TrashDeletedLessonWindow(group, weekDay, this);
            window.Owner = MainWindowViewModel.MainWindowLink;
            window.ShowDialog();
            var selectedSchedule = RestoreLessonObject;
            if (selectedSchedule == null) return;
            Lesson = InicializationLesson(selectedSchedule);
        }


        #endregion

        #region Method

        private void InitializationCommand() {

            RestoreChangedLessonCommand = new DelegateCommand(RestoreChangedLesson);
            SaveChangedLessonCommand = new DelegateCommand(SaveChangedLesson);
            CheckIntersectionCommand = new DelegateCommand(CheckIntersection);
            RestoreLessonCommand = new DelegateCommand(RestoreLesson);
        }

        private void InitializationCollection() {

            GlobalValue.GroupSchoolSubjects.ForEach(x => GroupSchoolSubjectsList.Add(x));
            GlobalValue.Teachers.ForEach(x => TeachersList.Add(x));
            GlobalValue.Cabinets.ForEach(x => CabinetsList.Add(x));
            GlobalValue.Times.Where(x => x.WeekDayNumber == Time.GetWeekDayNumberByDayName(weekDay)).OrderBy(
                s => s.LessonNumber).ToList().ForEach(x => TimeList.Add(x));
        }

        /// <summary>
        /// Заменяет объекты занятия на аналогичные объекты из выпадающих списков, для корректного отображения в интерфейсе
        /// </summary>
        private Schedule InicializationLesson(Schedule schedule) {

            schedule.SchoolSubject = GlobalValue.GroupSchoolSubjects.Find(x => x.Id == schedule.SchoolSubject.Id) ?? new Class();
            schedule.Teacher = GlobalValue.Teachers.Find(x => x.Id == schedule.Teacher.Id) ?? new Teacher();
            schedule.Cabinet = GlobalValue.Cabinets.Find(x => x.Id == schedule.Cabinet.Id) ?? new ClassRoom();
            schedule.Time = GlobalValue.Times.Find(x => x.Id == schedule.Time.Id) ?? new Time();

            return schedule;
        }

        /// <summary>
        /// По умолчанию возвращает результат для объекта модуля LessonCardWithEditViewModel
        /// </summary>
        public ScheduleForSave GetScheduleForSave(Schedule _lesson = null) {

            if (_lesson == null)
                _lesson = lesson;

            var scheduleForSave = new ScheduleForSave {

                Number = _lesson.Time.LessonNumber,
                Shift = GlobalValue.Shift,
                Changed = _lesson.Changed,
                ToDelete = _lesson.ToDelete,
                WeekDayId = _lesson._WeekDay.Id != 0
                     ? _lesson._WeekDay.Id : Time.GetWeekDayNumberByDayName(weekDay),
                GroupId = _lesson._Group.Id,
                SchoolSubjectId = _lesson.SchoolSubject.Id,
                TeacherId = _lesson.Teacher.Id,
                CabinetId = _lesson.Cabinet.Id,
                TimeId = _lesson.Time.Id,
            };

            return scheduleForSave;
        }

        #endregion

    }
}