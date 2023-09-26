using System.Windows.Controls;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;

namespace ScheduleCollegeEditor.View
{

    public partial class TimeLesson : UserControl
    {
        public TimeLesson(Time time = null, string dayWeek = null)
        {
            DataContext = new TimeLessonViewModel(time, dayWeek);
            InitializeComponent();
        }
    }
}
