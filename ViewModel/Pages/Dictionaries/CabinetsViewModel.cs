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
    class CabinetsViewModel : BaseViewModel
    {

        public CabinetsViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private List<ClassRoom> cabinets = new List<ClassRoom>();
        private ClassRoom cabinet = new ClassRoom();
        private ClassRoom selectedCabinet;
        private string findText;
        private string nameSaveButton = "Добавить";
        private bool editMode = false;

        #endregion

        #region Property

        public ObservableCollection<ClassRoom> CabinetsList { get; private set; }
            = new ObservableCollection<ClassRoom>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        public ClassRoom SelectedCabinet {
            get => selectedCabinet;
            set => Set(ref selectedCabinet, value);
        }

        public string FindText {
            get => findText;
            set {
                Set(ref findText, value);
                FindCabinet();
            }
        }

        public ClassRoom Cabinet {
            get => cabinet;
            set => Set(ref cabinet, value);
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

        public ICommand AddCabinetCommand { get; private set; }
        public ICommand DeleteCabinetCommand { get; private set; }
        public ICommand DeleteAllCabinetCommand { get; private set; }
        public ICommand EditCabinetCommand { get; private set; }
        public ICommand CancelEditCabinetCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void AddCabinet(object obj) {

            if (!ExistCurrentCabinetInList() || EditMode) {

                LoadAnimationVisible = true;
                cabinet.Name = cabinet.Name.Trim();
                var answer = await Task.Run(() => DBConnection.AddObjects(cabinet, "room"));

                if (answer) {

                    if (EditMode) EditMode = false;
                    Cabinet = new ClassRoom();
                    MainWindowViewModel.VMLink.Notification("Запись успешно сохранена");
                    RefreshCabinetsList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось сохранить запись", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private async void DeleteCabinet(object obj) {

            LoadAnimationVisible = true;
            var answer = await Task.Run(() => DBConnection.DeleteObjects(SelectedCabinet.Id, "room"));

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Запись успешно удалена");
                RefreshCabinetsList();
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось удалить запись", "fault");
            }
            LoadAnimationVisible = false;
        }

        private async void DeleteAllCabinet(object obj) {

            if (!(MessageBox.Show("Удалить все записи?", "Удаление всех записей", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                LoadAnimationVisible = true;
                bool successfullyDeleted = true;

                foreach (var cabinet in CabinetsList) {

                    var answer = await Task.Run(() => DBConnection.DeleteObjects(cabinet.Id, "room"));

                    if (!answer && successfullyDeleted)
                        successfullyDeleted = false;
                }

                if (successfullyDeleted) {

                    MainWindowViewModel.VMLink.Notification("Все записи успешно удалены");
                    RefreshCabinetsList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось удалить все записи", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private void EditCabinet(object obj) {

            EditMode = true;
            NameSaveButton = "Сохранить";
            Cabinet = SelectedCabinet;
        }

        private void CancelEditCabinet(object obj) {

            editMode = false;
            NameSaveButton = "Добавить";
            Cabinet = new ClassRoom();
        }


        private bool CanDeleteCabinet(object arg) {

            return (arg as ClassRoom) != null;
        }

        private bool CanDeleteAllCabinet(object arg) {

            return CabinetsList.Count != 0;
        }

        private bool CanEditCabinet(object arg) {

            return (arg as ClassRoom) != null;
        }

        private bool ActiveEditModeCabinet(object arg) {

            return EditMode;
        }

        private bool CanAddCabinet(object arg) {

            var cabinet = (ClassRoom)arg;

            if (cabinet != null) {

                if (!string.IsNullOrWhiteSpace(cabinet.Name))
                    return true;
            }
            return false;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            AddCabinetCommand = new DelegateCommand(AddCabinet, CanAddCabinet);
            DeleteCabinetCommand = new DelegateCommand(DeleteCabinet, CanDeleteCabinet);
            DeleteAllCabinetCommand = new DelegateCommand(DeleteAllCabinet, CanDeleteAllCabinet);
            EditCabinetCommand = new DelegateCommand(EditCabinet, CanEditCabinet);
            CancelEditCabinetCommand = new DelegateCommand(CancelEditCabinet, ActiveEditModeCabinet);
        }

        private async void InitializationCollection() {

            cabinets = await DBConnection.GetCabinetsAsync();
            cabinets.ForEach(x => CabinetsList.Add(x));
        }

        private bool ExistCurrentCabinetInList() {

            if (cabinets.Any(x => x.Name == cabinet.Name) && !EditMode) {

                MainWindowViewModel.VMLink.Notification("Кабинет с таким названием уже добавлен", "warning");
                return true;
            }
            return false;
        }

        private async void RefreshCabinetsList() {

            CabinetsList.Clear();
            cabinets = await DBConnection.GetCabinetsAsync();
            cabinets.ForEach(x => CabinetsList.Add(x));

        }      
        
        private void FindCabinet() {

            CabinetsList.Clear();
            foreach (var schoolSubject in cabinets)
                if (schoolSubject.Name.ToUpper().Contains(FindText.ToUpper()))
                    CabinetsList.Add(schoolSubject);
        }

        #endregion

    }
}