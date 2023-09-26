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
    class WeekDayScheduleEditorViewModel : BaseViewModel
    {

        public static List<WeekDayScheduleEditorViewModel> Links { get; set; }
            = new List<WeekDayScheduleEditorViewModel>();

        public WeekDayScheduleEditorViewModel(Group _group, string _weekDay) {

            group = _group;
            WeekDay = _weekDay;
            Links.Add(this);

            InitializationCommand();
            InitializationCollection();
            CheckIntersection();
        }

        #region Field

        private bool expandButtonVisible = false;
        private bool otherLessonListVisible = false;
        private string expandButtonContent = "Развернуть";
        private string weekDay;
        private Group group;

        #endregion

        #region Property

        public ObservableCollection<LessonCardWithEdit> LessonList { get; private set; }
            = new ObservableCollection<LessonCardWithEdit>();
        public ObservableCollection<LessonCardWithEdit> OtherLessonList { get; private set; }
            = new ObservableCollection<LessonCardWithEdit>();

        public bool ExpandButtonVisible {
            get => expandButtonVisible;
            set => Set(ref expandButtonVisible, value);
        }

        public bool OtherLessonListVisible {
            get => otherLessonListVisible;
            set => Set(ref otherLessonListVisible, value);
        }

        public string ExpandButtonContent {
            get => expandButtonContent;
            set => Set(ref expandButtonContent, value);
        }

        public string WeekDay {
            get => weekDay;
            set => Set(ref weekDay, value);
        }

        #endregion

        #region Command

        public ICommand ShowHiddenLessonsCommand { get; private set; }

        #endregion

        #region CommandMethod

        private void ShowHiddenLessons(object obj) {

            ExpandButtonContent = OtherLessonListVisible ? "Свернуть" : "Развернуть";
            OtherLessonListVisible = !OtherLessonListVisible;
        }

        private bool CanShowHiddenLessons(object arg) {

            bool showStatus = (OtherLessonList.Count != 0);
            ExpandButtonVisible = showStatus;
            return showStatus;
        }

        #endregion

        #region Methods

        private void InitializationCommand() {

            ShowHiddenLessonsCommand = new DelegateCommand(ShowHiddenLessons, CanShowHiddenLessons);
        }

        private void InitializationCollection() {

            var schedule = GlobalValue.Schedules.Where(x => x._Group.Id == group.Id && 
                x._WeekDay.DayName == weekDay).OrderBy(x => x.Number).ToList();

            int i = 0;

            foreach (var lesson in schedule) {

                if (i < GlobalValue.NumberLessons) {

                    LessonList.Add(new LessonCardWithEdit(lesson, weekDay, group));
                } else {

                    OtherLessonList.Add(new LessonCardWithEdit(lesson, weekDay, group));
                }
                i++;
            }

            for (int j = i; j < GlobalValue.NumberLessons; j++)
                LessonList.Add(new LessonCardWithEdit(GetNewEmptyLesson(), weekDay, group));

        }

        private Schedule GetNewEmptyLesson() {

            var lesson = new Schedule {

                _Group = group,
                Shift = GlobalValue.Shift,
                _WeekDay = new WeekDay { DayName = weekDay }
            };

            return lesson;
        }

        /// <summary>
        /// Проверяет, имеются пересечения занятий по кабинету
        /// </summary>
        public static void CheckIntersection() {
            
            var intersectionList = new List<Schedule>();

            foreach (var link in LessonCardWithEditViewModel.links) {

                var result = GlobalValue.Schedules.FindAll(x => x.Cabinet.Id == link.Lesson.Cabinet.Id && x.Number == link.Lesson.Number
                    && x._WeekDay.Id == link.Lesson._WeekDay.Id && x._Group.Id != link.Lesson._Group.Id);
                intersectionList.AddRange(result);
            }

            intersectionList = intersectionList.OrderBy(x => x._Group.Name).ToList();
            GlobalValue.FillGroupScheduleViewModelRef.InfoIntersectionLessonCardList.Clear();
            foreach (var schedule in intersectionList)
                GlobalValue.FillGroupScheduleViewModelRef.InfoIntersectionLessonCardList.Add(
                    new InfoIntersectionLessonCard(schedule));
        }

        #endregion

    }
}