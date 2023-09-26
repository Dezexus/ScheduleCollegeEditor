using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows.Controls;

namespace ScheduleCollegeEditor.View
{

    public partial class InfoIntersectionLessonCard : UserControl
    {
        public InfoIntersectionLessonCard(Schedule schedule)
        {
            DataContext = new InfoIntersectionLessonCardViewModel(schedule);
            InitializeComponent();
        }
    }
}
