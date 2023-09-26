using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows.Controls;

namespace ScheduleCollegeEditor.View
{
    public partial class WeekDayScheduleEditor : UserControl
    {
        public WeekDayScheduleEditor(Group group, string weekDay)
        {
            DataContext = new WeekDayScheduleEditorViewModel(group, weekDay);
            InitializeComponent();
        }
    }
}
