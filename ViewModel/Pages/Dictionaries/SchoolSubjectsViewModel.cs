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
    class SchoolSubjectsViewModel : BaseViewModel
    {

        public SchoolSubjectsViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private Class selectedSchoolSubject;
        private List<Class> schoolSubjects = new List<Class>();
        private Class schoolSubject = new Class();
        private string findText;
        private string nameSaveButton = "Добавить";
        private bool editMode = false;

        #endregion
         
        #region Property

        public ObservableCollection<Class> SchoolSubjectsList { get; private set; }
             = new ObservableCollection<Class>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        public Class SelectedSchoolSubject {
            get => selectedSchoolSubject;
            set => Set(ref selectedSchoolSubject, value);
        }

        public string FindText {
            get => findText;
            set {
                Set(ref findText, value);
                FindSchoolSubject();
            }
        }

        public Class SchoolSubject {
            get => schoolSubject;
            set => Set(ref schoolSubject, value);
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

        public ICommand AddSchoolSubjectCommand { get; private set; }
        public ICommand DeleteSchoolSubjectCommand { get; private set; }
        public ICommand DeleteAllSchoolSubjectsCommand { get; private set; }
        public ICommand EditSchoolSubjectCommand { get; private set; }
        public ICommand CancelEditSchoolSubjectCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void AddSchoolSubject(object obj) {

            if (!ExistCurrentSchoolSubjectInList() || EditMode) {

                LoadAnimationVisible = true;
                schoolSubject.Name = schoolSubject.Name.Trim();
                var answer = await Task.Run(()=> DBConnection.AddObjects(schoolSubject, "class"));

                if (answer) {

                    if (EditMode) EditMode = false;
                    MainWindowViewModel.VMLink.Notification("Запись успешно сохранена");
                    SchoolSubject = new Class();
                    RefreshSchoolSubjectsList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось сохранить запись", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private async void DeleteSchoolSubject(object obj) {

            LoadAnimationVisible = true;
            var answer = await Task.Run(() => DBConnection.DeleteObjects(SelectedSchoolSubject.Id, "class"));

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Запись успешно удалена");
                RefreshSchoolSubjectsList();
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось удалить запись", "fault");
            }
            LoadAnimationVisible = false;
        }

        private async void DeleteAllSchoolSubjects(object obj) {

            if (!(MessageBox.Show("Удалить все записи?", "Удаление всех записей", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                LoadAnimationVisible = true;
                bool successfullyDeleted = true;

                foreach (var schoolSubject in SchoolSubjectsList) {

                    var answer = await Task.Run(() => DBConnection.DeleteObjects(schoolSubject.Id, "class"));

                    if (!answer && successfullyDeleted)
                        successfullyDeleted = false;
                }

                if (successfullyDeleted) {

                    MainWindowViewModel.VMLink.Notification("Все записи успешно удалены");
                    RefreshSchoolSubjectsList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось удалить все записи", "fault");
                }
                LoadAnimationVisible = false;
            }

        }

        private void EditSchoolSubject(object obj) {

            EditMode = true;
            NameSaveButton = "Сохранить";
            SchoolSubject = SelectedSchoolSubject;
        }

        private void CancelEditSchoolSubject(object obj) {

            editMode = false;
            NameSaveButton = "Добавить";
            SchoolSubject = new Class();
        }


        private bool CanDeleteSchoolSubject(object arg) {

            return (arg as Class) != null;
        }

        private bool CanDeleteAllSchoolSubjects(object arg) {

            return SchoolSubjectsList.Count != 0;
        }

        private bool CanEditSchoolSubject(object arg) {

            return (arg as Class) != null;
        }

        private bool ActiveEditModeSchoolSubject(object arg) {

            return EditMode;
        }

        private bool CanAddSchoolSubject(object arg) {

            var schoolSubject = (Class)arg;

            if (schoolSubject != null) {

                if (!string.IsNullOrWhiteSpace(schoolSubject.Name))
                    return true;
            }
            return false;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            AddSchoolSubjectCommand = new DelegateCommand(AddSchoolSubject, CanAddSchoolSubject);
            DeleteSchoolSubjectCommand = new DelegateCommand(DeleteSchoolSubject, CanDeleteSchoolSubject);
            DeleteAllSchoolSubjectsCommand = new DelegateCommand(DeleteAllSchoolSubjects, CanDeleteAllSchoolSubjects);
            EditSchoolSubjectCommand = new DelegateCommand(EditSchoolSubject, CanEditSchoolSubject);
            CancelEditSchoolSubjectCommand = new DelegateCommand(CancelEditSchoolSubject, ActiveEditModeSchoolSubject);
        }

        private async void InitializationCollection() {

            schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();
            schoolSubjects.ForEach(x => SchoolSubjectsList.Add(x));
        }

        private bool ExistCurrentSchoolSubjectInList() {

            if (schoolSubjects.Any(x => x.Name == schoolSubject.Name) && !EditMode) {

                MainWindowViewModel.VMLink.Notification("Предмет с таким названием уже добавлен", "warning");
                return true;
            }
            return false;
        }

        private async void RefreshSchoolSubjectsList() {

            SchoolSubjectsList.Clear();
            schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();
            schoolSubjects.ForEach(x => SchoolSubjectsList.Add(x));

        }

        private void FindSchoolSubject() {

            SchoolSubjectsList.Clear();
            foreach (var schoolSubject in schoolSubjects)
                if (schoolSubject.Name.ToUpper().Contains(FindText.ToUpper()))
                    SchoolSubjectsList.Add(schoolSubject);
        }

        #endregion

    }
}