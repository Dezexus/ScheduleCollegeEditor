using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows;

namespace ScheduleCollegeEditor.View
{

    public partial class TrashDeletedLessonWindow : Window
    {
        public TrashDeletedLessonWindow(Group group, string dayWeek, LessonCardWithEditViewModel _link)
        {
            DataContext = new TrashDeletedLessonViewModel(group, dayWeek, _link);
            InitializeComponent();
        }
    }
}
