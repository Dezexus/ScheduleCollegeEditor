using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.ViewModel;
using System.Windows;

namespace ScheduleCollegeEditor.View
{

    public partial class CabinetOccupancyWindow : Window
    {
        public CabinetOccupancyWindow(ClassRoom cabinet)
        {
            DataContext = new CabinetOccupancyViewModel(cabinet);
            InitializeComponent();
        }
    }
}
