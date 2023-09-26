using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class ExportTeacherScheduleViewModel : BaseViewModel
    {

        public ExportTeacherScheduleViewModel() {

            InicializationCommand();
            InicializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private Teacher selectedTeacherInTeachers;
        private Teacher selectedTeacherInSelectedTeachers;

        #endregion

        #region Property

        public ObservableCollection<Teacher> Teachers { get; set; }
            = new ObservableCollection<Teacher>();

        public ObservableCollection<Teacher> SelectedTeachers { get; set; }
            = new ObservableCollection<Teacher>();

        public Teacher SelectedTeacherInTeachers {
            get => selectedTeacherInTeachers;
            set => Set(ref selectedTeacherInTeachers, value);
        }

        public Teacher SelectedTeacherInSelectedTeachers {
            get => selectedTeacherInSelectedTeachers;
            set => Set(ref selectedTeacherInSelectedTeachers, value);
        }

        public LoadingAnimation LoadingAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        #endregion

        #region Command

        public ICommand OpenTeacherScheduleCommand { get; private set; }
        public ICommand ExportTeacherScheduleInExcelCommand { get; private set; }
        public ICommand AddTeacherInSelectedTeachersCommand { get; private set; }
        public ICommand AddAllTeacherInSelectedTeachersCommand { get; private set; }
        public ICommand RemoveTeacherFromSelectedTeachersCommand { get; private set; }
        public ICommand RemoveAllTeacherFromSelectedTeachersCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void OpenTeacherSchedule(object obj) {

            LoadAnimationVisible = true;
            var window = new TeacherScheduleWindow(SelectedTeacherInTeachers);
            window.Owner = MainWindowViewModel.MainWindowLink;
            window.Show();
            LoadAnimationVisible = false;
        }

        private async void ExportTeacherScheduleInExcel(object obj) {

            LoadAnimationVisible = true;
            var Teachers = new List<Teacher>(SelectedTeachers);
            await Task.Run(()=>Report.ExportTeacherScheduleInExcel(Teachers));
            LoadAnimationVisible = false;
        }

        private void AddTeacherInSelectedTeachers(object obj) {

            SelectedTeachers.Add(selectedTeacherInTeachers);
        }

        private void AddAllTeacherInSelectedTeachers(object obj) {

            foreach (var Teacher in Teachers)
                SelectedTeachers.Add(Teacher);
        }

        private void RemoveTeacherFromSelectedTeachers(object obj) {

            SelectedTeachers.Remove(selectedTeacherInSelectedTeachers);
        }

        private void RemoveAllTeacherFromSelectedTeachers(object obj) {

            SelectedTeachers.Clear();
        }


        private bool CanAddTeacherInSelectedTeachers(object arg) {

            return (arg as Teacher) != null;
        }

        private bool CanRemoveTeacherFromSelectedTeachers(object arg) {

            return (arg as Teacher) != null;
        }

        private bool CanAddAllTeacherInSelectedTeachers(object arg) {

            return Teachers.Count != 0;
        }

        private bool CanRemoveAllTeacherFromSelectedTeachers(object arg) {

            return SelectedTeachers.Count != 0;
        }

        private bool CanOpenTeacherSchedule(object arg) {

            return SelectedTeacherInTeachers != null;
        }

        private bool CanExportTeacherSchedule(object arg) {

            return SelectedTeachers.Count != 0;
        }

        #endregion

        #region Method

        private void InicializationCommand() {

            OpenTeacherScheduleCommand = new DelegateCommand(OpenTeacherSchedule, CanOpenTeacherSchedule);
            ExportTeacherScheduleInExcelCommand = new DelegateCommand(ExportTeacherScheduleInExcel, CanExportTeacherSchedule);
            AddTeacherInSelectedTeachersCommand = new DelegateCommand(AddTeacherInSelectedTeachers, CanAddTeacherInSelectedTeachers);
            AddAllTeacherInSelectedTeachersCommand = new DelegateCommand(AddAllTeacherInSelectedTeachers, CanAddAllTeacherInSelectedTeachers);
            RemoveTeacherFromSelectedTeachersCommand = new DelegateCommand(RemoveTeacherFromSelectedTeachers, CanRemoveTeacherFromSelectedTeachers);
            RemoveAllTeacherFromSelectedTeachersCommand = new DelegateCommand(RemoveAllTeacherFromSelectedTeachers, CanRemoveAllTeacherFromSelectedTeachers);
        }

        private async void InicializationCollection() {

            var teachers = await DBConnection.GetTeachersAsync();
            teachers.ForEach(x => Teachers.Add(x));
        }

        #endregion

    }
}