using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class SettingWindowViewModel : BaseViewModel
    {

        public SettingWindowViewModel() {

            InitializationCommand();
        }

        #region Field

        private string selectedTheme;
        private int selectedNumberLessons;
        private string domen;
        private string password;

        #endregion

        #region Property

        public ObservableCollection<string> ThemeList { get; private set; }
            = new ObservableCollection<string>() { "Тёмная", "Светлая", "Система"};

        public ObservableCollection<int> MumberLessonsList { get; private set; }
            = new ObservableCollection<int>() { 4,5,6,7,8,9 };

        public string SelectedTheme {

            get => selectedTheme;
            set {
                Set(ref selectedTheme, value);
                Appearance.SetTheme = Appearance.ThemeStrToTheme(value);
            }
        }

        public int SelectedNumberLessons {

            get => selectedNumberLessons;
            set {
                Set(ref selectedNumberLessons, value);
                GlobalValue.NumberLessons = value;
            }
        }

        public string Domen {

            get => domen;
            set => Set(ref domen, value);

        }

        public string Password {

            get => password;
            set => Set(ref password, value);
        }

        #endregion

        #region Command

        public ICommand SaveServerSettingsCommand { get; private set; }
        public ICommand DeleteAllScheduleCommand { get; private set; }
        public ICommand LoadedAppCommand { get; private set; }

        #endregion

        #region MethodCommand

        private void SaveServerSettings(object obj) {

            DBConnection.DefaultDomen = domen;
            DBConnection.Password = password;
            Setting.SaveSetting();
            MessageBox.Show("Настройки сохранены");
        }

        private async void DeleteAllSchedule(object obj) {

            if (!(MessageBox.Show("Удалить расписание всех групп?", "Удаление всех данных", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                bool successfullyDeleted = await DBConnection.DeleteAllSchedule();

                if (successfullyDeleted) {

                    MessageBox.Show("Все записи успешно удалены");

                } else {

                    MessageBox.Show("Не удалось удалить все данные");
                }
            }
        }

        private void LoadedApp(object obj) {

            Domen = DBConnection.DefaultDomen;
            Password = DBConnection.Password;
            SelectedTheme = Appearance.ThemeToThemeStr(Appearance.SelectedTheme);
            SelectedNumberLessons = GlobalValue.NumberLessons;

        }

        #endregion

        #region Method

        private void InitializationCommand() {

            SaveServerSettingsCommand = new DelegateCommand(SaveServerSettings);
            DeleteAllScheduleCommand = new DelegateCommand(DeleteAllSchedule);
            LoadedAppCommand = new DelegateCommand(LoadedApp);
        }

        #endregion
    }
}