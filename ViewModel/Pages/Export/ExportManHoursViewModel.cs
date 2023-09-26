using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.View;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class ExportManHoursViewModel : BaseViewModel
    {

        public ExportManHoursViewModel() {

            InicializationCommand();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;

        #endregion

        #region Property

        public LoadingAnimation LoadingAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        #endregion

        #region Command

        public ICommand ExportManHoursGroupByLessonInExcelInWeekCommand { get; private set; }
        public ICommand ExportManHoursGroupByLessonInExcelByDaysCommand { get; private set; }
        public ICommand ExportManHoursGroupByLessonAndGroupInExcelInWeekCommand { get; private set; }
        public ICommand ExportManHoursGroupByLessonAndGroupInExcelByDaysCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void ExportManHoursGroupByLessonInExcelInWeek(object obj) {

            LoadAnimationVisible = true;
            await Task.Run(()=>Report.ExportManHoursGroupByLessonInExcel("Week"));
            LoadAnimationVisible = false;
        }

        private async void ExportManHoursGroupByLessonInExcelByDays(object obj) {

            LoadAnimationVisible = true;
            await Task.Run(() => Report.ExportManHoursGroupByLessonInExcel("Days"));
            LoadAnimationVisible = false;
        }

        private async void ExportManHoursGroupByLessonAndGroupInExcelInWeek(object obj) {

            LoadAnimationVisible = true;
            await Task.Run(() => Report.ExportManHoursGroupByLessonAndGroupInExcel("Week"));
            LoadAnimationVisible = false;
        }

        private async void ExportManHoursGroupByLessonAndGroupInExcelByDays(object obj) {

            LoadAnimationVisible = true;
            await Task.Run(() => Report.ExportManHoursGroupByLessonAndGroupInExcel("Days"));
            LoadAnimationVisible = false;
        }

        #endregion

        #region Method

        private void InicializationCommand() {

            ExportManHoursGroupByLessonInExcelInWeekCommand
                = new DelegateCommand(ExportManHoursGroupByLessonInExcelInWeek);
            ExportManHoursGroupByLessonInExcelByDaysCommand
                = new DelegateCommand(ExportManHoursGroupByLessonInExcelByDays);
            ExportManHoursGroupByLessonAndGroupInExcelInWeekCommand
                = new DelegateCommand(ExportManHoursGroupByLessonAndGroupInExcelInWeek);
            ExportManHoursGroupByLessonAndGroupInExcelByDaysCommand
                = new DelegateCommand(ExportManHoursGroupByLessonAndGroupInExcelByDays);
        }

        #endregion

    }
}