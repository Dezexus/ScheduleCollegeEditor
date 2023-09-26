using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ScheduleCollegeEditor.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public static Window MainWindowLink { get; private set; }
        public static MainWindowViewModel VMLink { get; private set; }

        public MainWindowViewModel() {

            VMLink = this;
            InicializationCommand();
        }

        #region Field

        private Page secondaryMenu;
        private Page selectedDictionaryPage;
        private object currentTheme;
        private string notifiNotificationText;
        private bool notificationVisible;
        private Brush notificationBorderBrush;

        #endregion

        #region Property

        public Page SecondaryMenu {
            get => secondaryMenu;
            set => Set(ref secondaryMenu, value);
        }

        public Page SelectedDictionaryPage {
            get => selectedDictionaryPage;
            set => Set(ref selectedDictionaryPage, value);
        }


        public object CurrentTheme {
            get => currentTheme;
            set => Set(ref currentTheme, value);
        }

        public string NotificationText {
            get => notifiNotificationText;
            set => Set(ref notifiNotificationText, value);
        }

        public bool NotificationVisible {
            get => notificationVisible;
            set => Set(ref notificationVisible, value);
        }
        public Brush NotificationBorderBrush {
            get => notificationBorderBrush;
            set => Set(ref notificationBorderBrush, value);
        }

        #endregion

        #region Command

        public ICommand OpenDictionariesPageCommand { get; private set; }
        public ICommand OpenFillGroupSchedulePageCommand { get; private set; }
        public ICommand OpenExportGroupSchedulePageCommand { get; private set; }
        public ICommand OpenSettingPageCommand { get; private set; }
        public ICommand ClosedAppCommand { get; private set; }
        public ICommand LoadedAppCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void OpenFillGroupSchedulePage(object obj) {

            SecondaryMenu = null;
            SelectedDictionaryPage = new FillGroupSchedule();
        }

        private void OpenDictionariesPage(object obj) {

            SecondaryMenu = new Dictionaries();
        }

        private void OpenExportGroupSchedulePage(object obj) {

            SecondaryMenu = new ExportNavigation();
        }

        private void OpenSettingPage(object obj) {

            var window = new SettingWindow();
            window.Owner = MainWindowLink;
            window.ShowDialog();
        }

        private void LoadedApp(object obj) {

            Internet.MainWindow = this;
            MainWindowLink = (Window)obj;
            Setting.LoadSetting();
        }

        private void ClosedApp(object obj) {

            Setting.SaveSetting();

            DirectoryInfo dir = new DirectoryInfo("pdf");
            foreach (FileInfo file in dir.GetFiles())
                file.Delete();
        }

        #endregion

        #region Method

        private void InicializationCommand() {

            OpenDictionariesPageCommand = new DelegateCommand(OpenDictionariesPage);
            OpenFillGroupSchedulePageCommand = new DelegateCommand(OpenFillGroupSchedulePage);
            OpenExportGroupSchedulePageCommand = new DelegateCommand(OpenExportGroupSchedulePage);
            OpenSettingPageCommand = new DelegateCommand(OpenSettingPage);
            ClosedAppCommand = new DelegateCommand(ClosedApp);
            LoadedAppCommand = new DelegateCommand(LoadedApp);
        }

        /// <summary>
        /// Отображает уведомления по вверх всего.
        /// notifiText - Текст уведомления
        /// notifiMode - Режимы уведомления: normal, fault, warning
        /// milliseconds - Время отображения
        /// </summary>
        public async void Notification(string notifiText, string notifiMode = "normal", int milliseconds = 2000) {

            Dictionary<string, string> colorsForNotifi = new Dictionary<string, string>() {
                { "normal", Application.Current.Resources["Notifi.Border.Brush.Normal"].ToString()},
                { "fault", Application.Current.Resources["Notifi.Border.Brush.Fault"].ToString()},
                { "warning", Application.Current.Resources["Notifi.Border.Brush.Warning"].ToString()}
            };

            var converter = new BrushConverter();
            NotificationBorderBrush = (Brush)converter.ConvertFromString(colorsForNotifi[notifiMode]);
            NotificationText = notifiText;
            NotificationVisible = true;
            await Task.Delay(milliseconds);
            NotificationVisible = false;
            NotificationText = null;
        }

        #endregion
    }
}
    