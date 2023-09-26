using ScheduleCollegeEditor.ViewModel;
using System.Net;

namespace ScheduleCollegeEditor.Classes
{
    class Internet
    {

        public static MainWindowViewModel MainWindow;

        public static bool CheckForInternetConnection() {

            try {

                var client = new WebClient();
                client.OpenRead("http://google.com/generate_204");
                return true;
            } catch {

                MainWindow.Notification("Нет подключения к Интернету", "warning", 5000);
                return false;
            }
        }
    }
}