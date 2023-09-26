using System.Windows.Controls;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;

namespace ScheduleCollegeEditor.View
{
    public partial class LessonCardWithEdit : UserControl
    {
        public LessonCardWithEdit(Schedule lesson, string _weekDay, Group _group)
        {
            DataContext = new LessonCardWithEditViewModel(lesson, _weekDay, _group);
            InitializeComponent();
        }
    }
}
