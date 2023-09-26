using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class TrashDeletedLessonViewModel : BaseViewModel
    {

        public TrashDeletedLessonViewModel(Group group, string dayWeek, LessonCardWithEditViewModel _link) {

            link = _link;
            InitializationCommand();
            FillList(group, dayWeek);
        }

        #region Field

        private List<Schedule> schedules;
        private LessonCard selectedLessonCard;
        private int selectedIndex;
        private LessonCardWithEditViewModel link;

        #endregion

        #region Property

        public ObservableCollection<LessonCard> LessonCardList { get; private set; }
            = new ObservableCollection<LessonCard>();

        public int SelectedIndex {

            get => selectedIndex;
            set => Set(ref selectedIndex, value);
        }

        public LessonCard SelectedLessonCard {

            get => selectedLessonCard;
            set => Set(ref selectedLessonCard, value);
        }

        #endregion

        #region Command

        public ICommand RestoreLessonCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void RestoreLesson(object obj) {

            link.RestoreLessonObject = schedules[selectedIndex];
            ((Window)obj).Close();
        }

        private bool CanRestoreLesson(object arg) {

            return selectedLessonCard != null;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            RestoreLessonCommand = new DelegateCommand(RestoreLesson, CanRestoreLesson);
        }

        private async void FillList(Group group, string dayWeek) {

            schedules = await DBConnection.GetTrashForGroupAsync(
                group.Name, dayWeek);
            schedules.ForEach(x => LessonCardList.Add(new LessonCard(x)));

        }

        #endregion
    }
}