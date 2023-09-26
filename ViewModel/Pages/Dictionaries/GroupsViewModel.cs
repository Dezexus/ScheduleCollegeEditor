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
    class GroupsViewModel : BaseViewModel
    {

        public GroupsViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private List<Group> groups = new List<Group>();
        private Group selectedGroup;
        private Group group = new Group();
        private string findText;
        private string nameSaveButton = "Добавить";
        private bool editMode = false;

        #endregion

        #region Property

        public ObservableCollection<Group> GroupsList { get; private set; }
             = new ObservableCollection<Group>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        public Group SelectedGroup {
            get => selectedGroup;
            set => Set(ref selectedGroup, value);
        }

        public string FindText {
            get => findText;
            set {
                Set(ref findText, value);
                FindGroup();
            }
        }

        public Group Group {
            get => group;
            set => Set(ref group, value);
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

        public ICommand AddGroupCommand { get; private set; }
        public ICommand DeleteGroupCommand { get; private set; }
        public ICommand DeleteAllGroupCommand { get; private set; }
        public ICommand EditGroupCommand { get; private set; }
        public ICommand CancelEditGroupCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void AddGroup(object obj) {

            if (!ExistCurrentGroupInList() || EditMode) {

                LoadAnimationVisible = true;
                group.Name = group.Name.Trim();
                var answer = await Task.Run(()=> DBConnection.AddObjects(group, "group"));

                if (answer) {

                    if (EditMode) EditMode = false;
                    MainWindowViewModel.VMLink.Notification("Запись успешно сохранена");
                    Group = new Group();
                    RefreshGroupList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось сохранить запись", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private async void DeleteGroup(object obj) {

            LoadAnimationVisible = true;
            var answer = await Task.Run(() => DBConnection.DeleteObjects(SelectedGroup.Id, "group"));

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Запись успешно удалена");
                RefreshGroupList();
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось удалить запись", "fault");
            }
            LoadAnimationVisible = false;
        }

        private async void DeleteAllGroup(object obj) {

            if (!(MessageBox.Show("Удалить все записи?", "Удаление всех записей", MessageBoxButton.YesNo, 
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                LoadAnimationVisible = true;
                bool successfullyDeleted = true;

                foreach (var group in GroupsList) {

                    var answer = await Task.Run(() => DBConnection.DeleteObjects(group.Id, "group"));

                    if (!answer && successfullyDeleted)
                        successfullyDeleted = false;
                }

                if (successfullyDeleted) {

                    MainWindowViewModel.VMLink.Notification("Все записи успешно удалены");
                    RefreshGroupList();
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось удалить все записи", "fault");
                }
                LoadAnimationVisible = false;
            }
        }

        private void EditGroup(object obj) {

            EditMode = true;
            NameSaveButton = "Сохранить";
            Group = SelectedGroup;
        }

        private void CancelEditGroup(object obj) {

            editMode = false;
            NameSaveButton = "Добавить";
            Group = new Group();
        }


        private bool CanDeleteGroup(object arg) {

            return (arg as Group) != null;
        }

        private bool CanDeleteAllGroup(object arg) {

            return GroupsList.Count != 0;
        }

        private bool CanEditGroup(object arg) {

            return (arg as Group) != null;
        }

        private bool ActiveEditModeGroup(object arg) {

            return EditMode;
        }

        private bool CanAddGroup(object arg) {

            var group = (Group)arg;

            if (group != null) {

                if (!string.IsNullOrWhiteSpace(group.Name))
                    return true;
            }
            return false;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            AddGroupCommand = new DelegateCommand(AddGroup, CanAddGroup);
            DeleteGroupCommand = new DelegateCommand(DeleteGroup, CanDeleteGroup);
            DeleteAllGroupCommand = new DelegateCommand(DeleteAllGroup, CanDeleteAllGroup);
            EditGroupCommand = new DelegateCommand(EditGroup, CanEditGroup);
            CancelEditGroupCommand = new DelegateCommand(CancelEditGroup, ActiveEditModeGroup);
        }

        private async void InitializationCollection() {

            groups = await DBConnection.GetGroupsAsync();
            groups.ForEach(x => GroupsList.Add(x));
        }

        private bool ExistCurrentGroupInList() {

            if (groups.Any(x => x.Name == group.Name) && !EditMode) {

                MainWindowViewModel.VMLink.Notification("Группа с таким названием уже добавлена", "warning");
                return true;
            }
            return false;
        }

        private async void RefreshGroupList() {

            GroupsList.Clear();
            groups = await DBConnection.GetGroupsAsync();
            groups.ForEach(x => GroupsList.Add(x));

        }

        private void FindGroup() {

            GroupsList.Clear();
            foreach (var schoolSubject in groups)
                if (schoolSubject.Name.ToUpper().Contains(FindText.ToUpper()))
                    GroupsList.Add(schoolSubject);
        }

        #endregion

    }
}