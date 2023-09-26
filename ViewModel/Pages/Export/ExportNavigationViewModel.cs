using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.View;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class ExportNavigationViewModel : BaseViewModel
    {

        public ExportNavigationViewModel() {

            InitializationCommand();
        }

        #region Field

        #endregion

        #region Property

        #endregion

        #region Command

        public ICommand OpenExportGroupSchedulePageCommand { get; private set; }
        public ICommand OpenExportTeacherSchedulePageCommand { get; private set; }
        public ICommand OpenExportCabinetSchedulePageCommand { get; private set; }
        public ICommand OpenExportManHoursPageCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void OpenExportGroupSchedulePage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new ExportGroupSchedule();
        }

        private void OpenExportTeacherSchedulePage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new ExportTeacherSchedule();
        }

        private void OpenExportCabinetSchedulePage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new ExportCabinetSchedule();
        }

        private void OpenExportManHoursPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new ExportManHours();
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            OpenExportGroupSchedulePageCommand = new DelegateCommand(OpenExportGroupSchedulePage);
            OpenExportTeacherSchedulePageCommand = new DelegateCommand(OpenExportTeacherSchedulePage);
            OpenExportCabinetSchedulePageCommand = new DelegateCommand(OpenExportCabinetSchedulePage);
            OpenExportManHoursPageCommand 
                = new DelegateCommand(OpenExportManHoursPage);
        }

        #endregion

    }
}