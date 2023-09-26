using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class ExportGroupScheduleViewModel : BaseViewModel
    {

        public ExportGroupScheduleViewModel() {

            InicializationCommand();
            InicializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private Group selectedGroupInGroups;
        private Group selectedGroupInSelectedGroups;

        #endregion

        #region Property

        public ObservableCollection<Group> Groups { get; set; }
            = new ObservableCollection<Group>();

        public ObservableCollection<Group> SelectedGroups { get; set; }
            = new ObservableCollection<Group>();

        public Group SelectedGroupInGroups {
            get => selectedGroupInGroups;
            set => Set(ref selectedGroupInGroups, value);
        }

        public Group SelectedGroupInSelectedGroups {
            get => selectedGroupInSelectedGroups;
            set => Set(ref selectedGroupInSelectedGroups, value);
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

        public ICommand ExportGroupScheduleInExcelCommand { get; private set; }
        public ICommand ExportGroupScheduleInPDFCommand { get; private set; }
        public ICommand AddGroupInSelectedGroupsCommand { get; private set; }
        public ICommand AddAllGroupInSelectedGroupsCommand { get; private set; }
        public ICommand RemoveGroupFromSelectedGroupsCommand { get; private set; }
        public ICommand RemoveAllGroupFromSelectedGroupsCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void ExportGroupScheduleInExcel(object obj) {

            LoadAnimationVisible = true;
            var groups = new List<Group>(SelectedGroups);
            await Task.Run(()=>Report.ExportGroupScheduleInExcel(groups));
            LoadAnimationVisible = false;
        }

        private async void ExportGroupScheduleInPDF(object obj) {

            LoadAnimationVisible = true;
            var groups = new List<Group>(SelectedGroups);
            await Task.Run(() => Report.ExportGroupScheduleInPDF(groups));
            LoadAnimationVisible = false;
        }

        private void AddGroupInSelectedGroups(object obj) {

            SelectedGroups.Add(selectedGroupInGroups);
        }

        private void AddAllGroupInSelectedGroups(object obj) {

            foreach (var group in Groups)
                SelectedGroups.Add(group);
        }

        private void RemoveGroupFromSelectedGroups(object obj) {

            SelectedGroups.Remove(selectedGroupInSelectedGroups);
        }

        private void RemoveAllGroupFromSelectedGroups(object obj) {

            SelectedGroups.Clear();
        }


        private bool CanAddGroupInSelectedGroups(object arg) {

            return (arg as Group) != null;
        }

        private bool CanRemoveGroupFromSelectedGroups(object arg) {

            return (arg as Group) != null;
        }

        private bool CanAddAllGroupInSelectedGroups(object arg) {

            return Groups.Count != 0;
        }

        private bool CanRemoveAllGroupFromSelectedGroups(object arg) {

            return SelectedGroups.Count != 0;
        }

        private bool CanExportGroupSchedule(object arg) {

            return SelectedGroups.Count != 0;
        }

        #endregion

        #region Method

        private void InicializationCommand() {

            ExportGroupScheduleInExcelCommand = new DelegateCommand(ExportGroupScheduleInExcel, CanExportGroupSchedule);
            ExportGroupScheduleInPDFCommand = new DelegateCommand(ExportGroupScheduleInPDF, CanExportGroupSchedule);
            AddGroupInSelectedGroupsCommand = new DelegateCommand(AddGroupInSelectedGroups, CanAddGroupInSelectedGroups);
            AddAllGroupInSelectedGroupsCommand = new DelegateCommand(AddAllGroupInSelectedGroups, CanAddAllGroupInSelectedGroups);
            RemoveGroupFromSelectedGroupsCommand = new DelegateCommand(RemoveGroupFromSelectedGroups, CanRemoveGroupFromSelectedGroups);
            RemoveAllGroupFromSelectedGroupsCommand = new DelegateCommand(RemoveAllGroupFromSelectedGroups, CanRemoveAllGroupFromSelectedGroups);
        }

        private async void InicializationCollection() {

            var groups = await DBConnection.GetGroupsAsync();
            groups.ForEach(x => Groups.Add(x));
        }

        #endregion

    }
}