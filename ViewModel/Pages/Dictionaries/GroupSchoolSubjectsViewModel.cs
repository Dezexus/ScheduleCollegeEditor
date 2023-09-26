using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class GroupSchoolSubjectsViewModel : BaseViewModel
    {

        public GroupSchoolSubjectsViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private Group selectedGroup;
        private Class selectedFromAllSchoolSubject;
        private Class selectedGroupSchoolSubject;
        private List<Group> groups = new List<Group>();
        private List<Class> schoolSubjects = new List<Class>();
        private string findText;
        private string groupName;
        private bool editMode = false;
        private bool listboxEnable = false;

        #endregion

        #region Property

        public ObservableCollection<Group> GroupsList { get; private set; }
            = new ObservableCollection<Group>();
        public ObservableCollection<Class> SchoolSubjectList { get; private set; }
            = new ObservableCollection<Class>();
        public ObservableCollection<Class> GroupSchoolSubjectList { get; private set; }
            = new ObservableCollection<Class>();

        public Group SelectedGroup {
            get => selectedGroup;
            set {
                Set(ref selectedGroup, value);
                FillSchoolSubjects();
            }
        }

        public Class SelectedFromAllSchoolSubject {
            get => selectedFromAllSchoolSubject;
            set => Set(ref selectedFromAllSchoolSubject, value);
        }

        public Class SelectedGroupSchoolSubject {
            get => selectedGroupSchoolSubject;
            set => Set(ref selectedGroupSchoolSubject, value);
        }

        public string FindText {
            get => findText;
            set {
                Set(ref findText, value);
                FindSchoolSubject();
            }
        }

        public string GroupName {
            get => groupName;
            set => Set(ref groupName, value);
        }

        public bool EditMode {
            get => editMode;
            set => Set(ref editMode, value);
        }

        public bool ListboxEnable {
            get => listboxEnable;
            set => Set(ref listboxEnable, value);
        }

        #endregion

        #region Command

        public ICommand AddSchoolSubjectInGroupCommand { get; private set; }
        public ICommand RemoveSchoolSubjectFromGroupCommand { get; private set; }
        public ICommand RemoveAllSchoolSubjectsFromGroupCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void AddSchoolSubjectInGroup(object obj) {

            bool exist = false;

            foreach (var item in GroupSchoolSubjectList) {
                if (item.Equals(SelectedFromAllSchoolSubject)) {

                    exist = true;
                    break;
                }
            }

            if (SelectedFromAllSchoolSubject != null && !exist) {

                GroupSchoolSubjectList.Add(SelectedFromAllSchoolSubject);

                var groupSchoolSubject = new GroupClasses { 
                    
                    GroupId = SelectedGroup.Id, 
                    ClassId = SelectedFromAllSchoolSubject.Id 
                };

                var groupClasses = new List<GroupClasses>();
                groupClasses.Add(new GroupClasses { 
                    GroupId = SelectedGroup.Id, ClassId = SelectedFromAllSchoolSubject.Id });
                var answer = await DBConnection.AddGroupInSchoolSubject(groupClasses);

                if (answer) {

                    MainWindowViewModel.VMLink.Notification("Запись успешно сохранена");
                    groupClasses.Add(groupSchoolSubject);
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось сохранить запись", "fault");
                }

            }
                
        }

        private async void RemoveSchoolSubjectFromGroup(object obj) {

            var answer = await DBConnection.DeleteGroupSchoolSubject(
                SelectedGroup.Id, SelectedGroupSchoolSubject.Id);

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Запись успешно удалена");
                GroupSchoolSubjectList.Remove(SelectedGroupSchoolSubject);
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось удалить запись");
            }

        }

        private async void RemoveAllSchoolSubjectsFromGroup(object obj) {

            if (!(MessageBox.Show("Удалить все предметы из группы?", "Удаление всех записей", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                bool successfullyDeleted = true;

                foreach (var shoolSubject in GroupSchoolSubjectList) {

                    var answer = await DBConnection.DeleteGroupSchoolSubject(
                        SelectedGroup.Id, shoolSubject.Id);

                    if (!answer && successfullyDeleted)
                        successfullyDeleted = false;
                }

                if (successfullyDeleted) {

                    MainWindowViewModel.VMLink.Notification("Все предметы из группы успешно удалены");
                } else {

                    MainWindowViewModel.VMLink.Notification("Не удалось удалить все предметы из группы записи", "fault");
                }
                FillSchoolSubjects();
            }
        }


        private bool CanRemoveSchoolSubjectFromGroup(object arg) {

            return ListboxEnable;
        }

        private bool CanAddSchoolSubjectInGroup(object arg) {

            return ListboxEnable;
        }

        private bool CanRemoveAllSchoolSubjectsFromGroup(object arg) {

            return (ListboxEnable && (GroupSchoolSubjectList.Count != 0));
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            AddSchoolSubjectInGroupCommand = new DelegateCommand(AddSchoolSubjectInGroup, CanAddSchoolSubjectInGroup);
            RemoveSchoolSubjectFromGroupCommand = new DelegateCommand(RemoveSchoolSubjectFromGroup, CanRemoveSchoolSubjectFromGroup);
            RemoveAllSchoolSubjectsFromGroupCommand = new DelegateCommand(RemoveAllSchoolSubjectsFromGroup, CanRemoveAllSchoolSubjectsFromGroup);
        }

        private async void InitializationCollection() {

            groups = await DBConnection.GetGroupsAsync();
            schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();

            schoolSubjects.ForEach(x => SchoolSubjectList.Add(x));
            groups.ForEach(x => GroupsList.Add(x));
        }

        private async void FillSchoolSubjects() {

            if (SelectedGroup != null) {

                var list = await DBConnection.GetGroupSchoolSubjectsAsync(SelectedGroup.Id);
                GroupSchoolSubjectList.Clear();
                list.ForEach(x => GroupSchoolSubjectList.Add(x));
                ListboxEnable = true;
            } else {
                ListboxEnable = false;
            }
        }

        private void FindSchoolSubject() {

            SchoolSubjectList.Clear();
            foreach (var schoolSubject in schoolSubjects)
                if (schoolSubject.Name.ToUpper().Contains(FindText.ToUpper()))
                    SchoolSubjectList.Add(schoolSubject);
        }

        #endregion

    }
}