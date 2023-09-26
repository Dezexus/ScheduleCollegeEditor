using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows.Controls;

namespace ScheduleCollegeEditor.View
{

    public partial class TeacherLessonCard : UserControl
    {
        public TeacherLessonCard(Schedule schedule)
        {
            DataContext = new TeacherLessonCardViewModel(schedule);
            InitializeComponent();
        }
    }
}
