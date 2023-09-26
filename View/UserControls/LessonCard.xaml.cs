using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows.Controls;

namespace ScheduleCollegeEditor.View
{
    public partial class LessonCard : UserControl
    {
        public LessonCard(Schedule schedule)
        {
            DataContext = new LessonCardViewModel(schedule);
            InitializeComponent();
        }
    }
}
