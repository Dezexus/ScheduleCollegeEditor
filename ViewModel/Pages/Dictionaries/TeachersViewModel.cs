using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class TeachersViewModel : BaseViewModel
    {

        public TeachersViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private List<Teacher> teachers = new List<Teacher>();
        private Teacher teacher = new Teacher();
        private Teacher selectedTeacher;
        private string findText;
        private string nameSaveButton = "Добавить";
        private bool editMode = false;

        #endregion

        #region Property

        public ObservableCollection<Teacher> TeachersList { get; private set; }
            = new ObservableCollection<Teacher>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        public Teacher SelectedTeacher {
            get => selectedTeacher;
            set => Set(ref selectedTeacher, value);
        }

        public string FindText {
            get => findText;
            set {
                Set(ref findText, value);
                FindTeacher();
            }
        }

        public Teacher Teacher {
            get => teacher;
            set => Set(ref teacher, value);
        }

        public string NameSaveButton {
            get => nameSaveButton;
            set => Set(ref nameSaveButton, value);
        }

        public bool EditMode {
            get => editMode;
            set => Set(ref editMode, value);
        }

        #endregion

        #region Command

        public ICommand AddTeacherCommand { get; private set; }
        public ICommand DeleteTeacherCommand { get; private set; }
        public ICommand DeleteAllTeachersCommand { get; private set; }
        public ICommand EditTeacherCommand { get; private set; }
        public ICommand CancelEditTeacherCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void AddTeacher(object obj) {

            if (!ExistCurrentTeacherInList() || EditMode) {

                LoadAnimationVisible = true;
                teacher.FullName = teacher.FullName.Trim();
                var answer = await Task.Run(()=> DBConnection.AddObjects(teacher, "teacher"));

                if (answer) {

                    if (EditMode) EditMode = false;
                    MainWindowViewModel.VMLink.Notification("Запись успешно сохранена");
                    Teacher = new Teacher();
                    RefreshTeachersList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось сохранить запись", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private async void DeleteTeacher(object obj) {

            LoadAnimationVisible = true;
            var answer = await Task.Run(() => DBConnection.DeleteObjects(SelectedTeacher.Id, "teacher"));

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Запись успешно удалена");
                RefreshTeachersList();
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось удалить запись", "fault");
            }
            LoadAnimationVisible = false;
        }

        private async void DeleteAllTeachers(object obj) {

            if (!(MessageBox.Show("Удалить все записи?", "Удаление всех записей", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                LoadAnimationVisible = true;
                bool successfullyDeleted = true;

                foreach (var teacher in TeachersList) {

                    var answer = await Task.Run(() => DBConnection.DeleteObjects(teacher.Id, "teacher"));

                    if (!answer && successfullyDeleted)
                        successfullyDeleted = false;
                }

                if (successfullyDeleted) {

                    MainWindowViewModel.VMLink.Notification("Все записи успешно удалены");
                    RefreshTeachersList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось удалить все записи", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private void EditTeacher(object obj) {

            EditMode = true;
            NameSaveButton = "Сохранить";
            Teacher = SelectedTeacher;
        }

        private void CancelEditTeacher(object obj) {

            editMode = false;
            NameSaveButton = "Добавить";
            Teacher = new Teacher();
        }


        private bool CanDeleteTeacher(object arg) {

            return (arg as Teacher) != null;
        }

        private bool CanDeleteAllTeachers(object arg) {

            return TeachersList.Count != 0;
        }

        private bool CanEditTeacher(object arg) {

            return (arg as Teacher) != null;
        }

        private bool ActiveEditModeTeacher(object arg) {

            return EditMode;
        }

        private bool CanAddTeacher(object arg) {

            var teacher = (Teacher)arg;

            if (teacher != null) {

                if (!string.IsNullOrWhiteSpace(teacher.FullName))
                    return true;
            }
            return false;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            CancelEditTeacherCommand = new DelegateCommand(CancelEditTeacher, ActiveEditModeTeacher);
            DeleteAllTeachersCommand = new DelegateCommand(DeleteAllTeachers, CanDeleteAllTeachers);
            DeleteTeacherCommand = new DelegateCommand(DeleteTeacher, CanDeleteTeacher);
            EditTeacherCommand = new DelegateCommand(EditTeacher, CanEditTeacher);
            AddTeacherCommand = new DelegateCommand(AddTeacher, CanAddTeacher);
        }

        private async void InitializationCollection() {

            teachers = await DBConnection.GetTeachersAsync();
            teachers.ForEach(x => TeachersList.Add(x));
        }

        private bool ExistCurrentTeacherInList() {

            if (teachers.Any(x => x.FullName == teacher.FullName) && !EditMode) {

                MainWindowViewModel.VMLink.Notification("Преподаватель с таким ФИО уже добавлен", "warning");
                return true;
            }
            return false;
        }

        private async void RefreshTeachersList() {

            TeachersList.Clear();
            teachers = await DBConnection.GetTeachersAsync();
            teachers.ForEach(x => TeachersList.Add(x));
        }

        private void FindTeacher() {

            TeachersList.Clear();
            foreach (var teacher in teachers)
                if (teacher.FullName.ToUpper().Contains(FindText.ToUpper()))
                    TeachersList.Add(teacher);
        }

        #endregion


    }
}