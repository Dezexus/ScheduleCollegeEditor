using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class CreateTemplateScheduleCallsViewModel : BaseViewModel
    {

        public CreateTemplateScheduleCallsViewModel(List<Time> times) {

            this.times = times;
            InitializationCommand();
        }

        #region Field

        private readonly List<Time> times;
        private string name;

        #endregion

        #region Propert

        public string Name {

            get => name;
            set => Set(ref name, value);
        }

        #endregion

        #region Commands

        public ICommand CreateTemplateScheduleCallsCommand { get; private set; }

        #endregion

        #region MethodCommands

        private void CreateTemplateScheduleCalls(object obj) {

            LocalDBConnection.AddTemplate(name, times);
            ((Window)obj).Close();
        }

        private bool CanCreateTemplateScheduleCalls(object arg) {

            return !string.IsNullOrWhiteSpace(name);
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            CreateTemplateScheduleCallsCommand 
                = new DelegateCommand(CreateTemplateScheduleCalls, CanCreateTemplateScheduleCalls);
        }

        #endregion
    }
}