using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace ScheduleCollegeEditor.View
{

    public partial class CreateTemplateScheduleCallsWindow : Window
    {
        public CreateTemplateScheduleCallsWindow(List<Time> times)
        {
            DataContext = new CreateTemplateScheduleCallsViewModel(times);
            InitializeComponent();
        }
    }
}
