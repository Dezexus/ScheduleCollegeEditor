using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows;

namespace ScheduleCollegeEditor.View
{

    public partial class TeacherScheduleWindow : Window
    {
        public TeacherScheduleWindow(Teacher teacher)
        {
            DataContext = new TeacherScheduleViewModel(teacher);
            InitializeComponent();
        }
    }
}
