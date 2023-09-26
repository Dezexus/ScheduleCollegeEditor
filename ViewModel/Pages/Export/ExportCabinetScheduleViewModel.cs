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
    class ExportCabinetScheduleViewModel : BaseViewModel
    {

        public ExportCabinetScheduleViewModel() {

            InicializationCommand();
            InicializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private ClassRoom selectedCabinetInCabinets;
        private ClassRoom selectedCabinetInSelectedCabinets;

        #endregion

        #region Property

        public ObservableCollection<ClassRoom> Cabinets { get; set; }
            = new ObservableCollection<ClassRoom>();

        public ObservableCollection<ClassRoom> SelectedCabinets { get; set; }
            = new ObservableCollection<ClassRoom>();

        public ClassRoom SelectedCabinetInCabinets {
            get => selectedCabinetInCabinets;
            set => Set(ref selectedCabinetInCabinets, value);
        }

        public ClassRoom SelectedCabinetInSelectedCabinets {
            get => selectedCabinetInSelectedCabinets;
            set => Set(ref selectedCabinetInSelectedCabinets, value);
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

        public ICommand OpenCabinetScheduleCommand { get; private set; }
        public ICommand ExportCabinetScheduleInExcelCommand { get; private set; }
        public ICommand AddCabinetInSelectedCabinetsCommand { get; private set; }
        public ICommand AddAllCabinetInSelectedCabinetsCommand { get; private set; }
        public ICommand RemoveCabinetFromSelectedCabinetsCommand { get; private set; }
        public ICommand RemoveAllCabinetFromSelectedCabinetsCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void OpenCabinetSchedule(object obj) {

            LoadAnimationVisible = true;
            var window = new CabinetOccupancyWindow(selectedCabinetInCabinets);
            window.Owner = MainWindowViewModel.MainWindowLink;
            window.Show();
            LoadAnimationVisible = false;
        }

        private async void ExportCabinetScheduleInExcel(object obj) {

            LoadAnimationVisible = true;
            var cabinets = new List<ClassRoom>(SelectedCabinets);
            await Task.Run(()=>Report.ExportCabinetScheduleInExcel(cabinets));
            LoadAnimationVisible = false;
        }

        private void AddCabinetInSelectedCabinets(object obj) {

            SelectedCabinets.Add(selectedCabinetInCabinets);
        }

        private void AddAllCabinetInSelectedCabinets(object obj) {

            foreach (var cabinet in Cabinets)
                SelectedCabinets.Add(cabinet);
        }

        private void RemoveCabinetFromSelectedCabinets(object obj) {

            SelectedCabinets.Remove(selectedCabinetInSelectedCabinets);
        }

        private void RemoveAllCabinetFromSelectedCabinets(object obj) {

            SelectedCabinets.Clear();
        }


        private bool CanAddCabinetInSelectedCabinets(object arg) {

            return (arg as ClassRoom) != null;
        }

        private bool CanRemoveCabinetFromSelectedCabinets(object arg) {

            return (arg as ClassRoom) != null;
        }

        private bool CanAddAllCabinetInSelectedCabinets(object arg) {

            return Cabinets.Count != 0;
        }

        private bool CanRemoveAllCabinetFromSelectedCabinets(object arg) {

            return SelectedCabinets.Count != 0;
        }

        private bool CanOpenCabinetSchedule(object arg) {

            return selectedCabinetInCabinets != null;
        }

        private bool CanExportCabinetSchedule(object arg) {

            return SelectedCabinets.Count != 0;
        }

        #endregion

        #region Method

        private void InicializationCommand() {

            OpenCabinetScheduleCommand = new DelegateCommand(OpenCabinetSchedule, CanOpenCabinetSchedule);
            ExportCabinetScheduleInExcelCommand = new DelegateCommand(ExportCabinetScheduleInExcel, CanExportCabinetSchedule);
            AddCabinetInSelectedCabinetsCommand = new DelegateCommand(AddCabinetInSelectedCabinets, CanAddCabinetInSelectedCabinets);
            AddAllCabinetInSelectedCabinetsCommand = new DelegateCommand(AddAllCabinetInSelectedCabinets, CanAddAllCabinetInSelectedCabinets);
            RemoveCabinetFromSelectedCabinetsCommand = new DelegateCommand(RemoveCabinetFromSelectedCabinets, CanRemoveCabinetFromSelectedCabinets);
            RemoveAllCabinetFromSelectedCabinetsCommand = new DelegateCommand(RemoveAllCabinetFromSelectedCabinets, CanRemoveAllCabinetFromSelectedCabinets);
        }

        private async void InicializationCollection() {

            var cabinets = await DBConnection.GetCabinetsAsync();
            cabinets.ForEach(x => Cabinets.Add(x));
        }

        #endregion

    }
}