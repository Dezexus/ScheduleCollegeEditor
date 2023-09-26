using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows.Controls;

namespace ScheduleCollegeEditor.View
{
    public partial class CabinetLessonCard : UserControl
    {
        public CabinetLessonCard(Schedule lesson)
        {
            DataContext = new CabinetLessonCardViewModel(lesson);
            InitializeComponent();
        }
    }
}
