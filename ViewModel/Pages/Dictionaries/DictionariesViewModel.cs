using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.View;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class DictionariesViewModel : BaseViewModel
    {

        public DictionariesViewModel() {

            InitializationCommand();
        }

        #region Field

        #endregion

        #region Property

        #endregion

        #region Command

        public ICommand OpenTeachersPageCommand { get; private set; }
        public ICommand OpenSchoolSubjectsPageCommand { get; private set; }
        public ICommand OpenCabinetsPageCommand { get; private set; }
        public ICommand OpenGroupsPageCommand { get; private set; }
        public ICommand OpenGroupSchoolSubjectsPageCommand { get; private set; }
        public ICommand OpenTimesPageCommand { get; private set; }

        #endregion

        #region MothodCommand

        private void OpenGroupSchoolSubjectsPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new GroupSchoolSubjects();
        }

        private void OpenGroupsPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new Groups();
        }

        private void OpenCabinetsPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new Cabinets();
        }

        private void OpenSchoolSubjectsPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new SchoolSubjects();
        }

        private void OpenTeachersPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new Teachers();
        }

        private void OpenTimesPage(object obj) {

            MainWindowViewModel.VMLink.SelectedDictionaryPage
                = new Times();
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            OpenTeachersPageCommand = new DelegateCommand(OpenTeachersPage);
            OpenSchoolSubjectsPageCommand = new DelegateCommand(OpenSchoolSubjectsPage);
            OpenCabinetsPageCommand = new DelegateCommand(OpenCabinetsPage);
            OpenGroupsPageCommand = new DelegateCommand(OpenGroupsPage);
            OpenGroupSchoolSubjectsPageCommand = new DelegateCommand(OpenGroupSchoolSubjectsPage);
            OpenTimesPageCommand = new DelegateCommand(OpenTimesPage);

        }

        #endregion

    }
}