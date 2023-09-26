using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class TemplateScheduleCallsViewModel : BaseViewModel
    {
        public static List<Time> CallSchedule { get; set; }

        public TemplateScheduleCallsViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private TemplateSheduleCalls selectedTemplateSheduleCalls;

        #endregion

        #region Property

        public ObservableCollection<TemplateSheduleCalls> TemplateSheduleCallsList { get; private set; }
            = new ObservableCollection<TemplateSheduleCalls>();

        public TemplateSheduleCalls SelectedTemplateSheduleCalls {

            get => selectedTemplateSheduleCalls;
            set => Set(ref selectedTemplateSheduleCalls, value);
        }

        #endregion

        #region Command

        public ICommand ApplyTemplateCommand { get; private set; }
        public ICommand DeleteTemplateCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void ApplyTemplate(object obj) {

            CallSchedule = await Task.Run(() => LocalDBConnection.GetCallsBIdTemplate(selectedTemplateSheduleCalls.Id));
            ((Window)obj).Close();
        }

        private async void DeleteTemplate(object obj) {

            await Task.Run(() => LocalDBConnection.DeleteTemplate(selectedTemplateSheduleCalls.Id));
            TemplateSheduleCallsList.Remove(SelectedTemplateSheduleCalls);
        }

        private bool CanApplyTemplate(object arg) {

            return selectedTemplateSheduleCalls != null;
        }

        private bool CanDeleteTemplate(object arg) {

            return selectedTemplateSheduleCalls != null;
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            ApplyTemplateCommand = new DelegateCommand(ApplyTemplate, CanApplyTemplate);
            DeleteTemplateCommand = new DelegateCommand(DeleteTemplate, CanDeleteTemplate);
        }

        private void InitializationCollection() {

            var templates = LocalDBConnection.GetTemplateList();
            templates.ForEach(x => TemplateSheduleCallsList.Add(x));
        }

        #endregion
    }
}