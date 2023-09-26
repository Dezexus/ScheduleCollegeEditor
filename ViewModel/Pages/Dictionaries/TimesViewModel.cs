using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScheduleCollegeEditor.ViewModel
{
    class TimesViewModel : BaseViewModel
    {

        public TimesViewModel() {

            InitializationCommand();
            InitializationCollection();
        }

        #region Field

        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;

        #endregion

        #region Property

        public ObservableCollection<TimeLesson> MondayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();
        public ObservableCollection<TimeLesson> TuesdayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();
        public ObservableCollection<TimeLesson> WednesdayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();
        public ObservableCollection<TimeLesson> ThursdayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();
        public ObservableCollection<TimeLesson> FridayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();
        public ObservableCollection<TimeLesson> SaturdayTimeSchedule { get; private set; }
            = new ObservableCollection<TimeLesson>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        #endregion

        #region Command

        public ICommand SaveCallsScheduleCommand { get; private set; }
        public ICommand AddTimeLessonCommand { get; private set; }
        public ICommand CreateTemplateScheduleCallsCommand { get; private set; }
        public ICommand OpenTemplateScheduleCallsWindowCommand { get; private set; }

        #endregion

        #region MethodCommand

        private async void SaveCallsSchedule(object obj) {

            LoadAnimationVisible = true;
            DeleteTimeSchedule(); //Удаление всех звонков поставленных на удаление

            var times = new List<Time>(await GetCallSchedule());

            if (times.Count == 0) return;

            bool answer = await DBConnection.AddObjects(times, "time");

            if (answer) {

                MainWindowViewModel.VMLink.Notification("Расписание успешно сохранена");
                InitializationCollection();
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось сохранить расписание", "fault");
            }
            LoadAnimationVisible = false;
        }

        private void AddTimeLesson(object obj) {

            switch (obj.ToString()) {

                case "Пн":
                    MondayTimeSchedule.Add(new TimeLesson(null,"Понедельник"));
                    break;
                case "Вт":
                    TuesdayTimeSchedule.Add(new TimeLesson(null, "Вторник"));
                    break;
                case "Ср":
                    WednesdayTimeSchedule.Add(new TimeLesson(null, "Среда"));
                    break;
                case "Чт":
                    ThursdayTimeSchedule.Add(new TimeLesson(null, "Четверг"));
                    break;
                case "Пт":
                    FridayTimeSchedule.Add(new TimeLesson(null, "Пятница"));
                    break;
                case "Сб":
                    SaturdayTimeSchedule.Add(new TimeLesson(null, "Суббота"));
                    break;
            }
        }

        private async void CreateTemplateScheduleCalls(object obj) {

            var window = new CreateTemplateScheduleCallsWindow(await GetCallSchedule());
            window.Owner = MainWindowViewModel.MainWindowLink;
            window.ShowDialog();
        }

        private void OpenTemplateScheduleCallsWindow(object obj) {

            var window = new TemplateScheduleCallsWindow();
            window.Owner = MainWindowViewModel.MainWindowLink;
            window.ShowDialog();

            var times = TemplateScheduleCallsViewModel.CallSchedule;

            if (times != null) ReplaceCallSchedule(times);
        }

        #endregion

        #region Method

        private void InitializationCommand() {

            OpenTemplateScheduleCallsWindowCommand = new DelegateCommand(OpenTemplateScheduleCallsWindow);
            CreateTemplateScheduleCallsCommand = new DelegateCommand(CreateTemplateScheduleCalls);
            SaveCallsScheduleCommand = new DelegateCommand(SaveCallsSchedule);
            AddTimeLessonCommand = new DelegateCommand(AddTimeLesson);
        }

        private async void InitializationCollection(List<Time> times = null) {

            if (times == null) {

                TimeLessonViewModel.linkViewModels.Clear(); //Clearing the list of links to created  TimeLessonViewModel
                ClearCollection();
                times = await DBConnection.GetTimeAsync();
            }

            times.Where(x => x.WeekDayNumber == 1).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => MondayTimeSchedule.Add(new TimeLesson(x)));
            times.Where(x => x.WeekDayNumber == 2).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => TuesdayTimeSchedule.Add(new TimeLesson(x)));
            times.Where(x => x.WeekDayNumber == 3).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => WednesdayTimeSchedule.Add(new TimeLesson(x)));
            times.Where(x => x.WeekDayNumber == 4).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => ThursdayTimeSchedule.Add(new TimeLesson(x)));
            times.Where(x => x.WeekDayNumber == 5).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => FridayTimeSchedule.Add(new TimeLesson(x)));
            times.Where(x => x.WeekDayNumber == 6).OrderBy(x => x.LessonNumber).ToList().ForEach(
                x => SaturdayTimeSchedule.Add(new TimeLesson(x)));
        }

        private void ReplaceCallSchedule(List<Time> times) {

            foreach (var link in TimeLessonViewModel.linkViewModels) {

                Time time = times.Find(x => x.LessonNumber == link.TimeLesson.LessonNumber && x.WeekDayNumber == link.TimeLesson.WeekDayNumber);
                link.TimeLesson = time;
                times.Remove(time);
            }
            InitializationCollection(times);
        }

        private async void DeleteTimeSchedule() {

            var timeLessons = new List<TimeLessonViewModel>(TimeLessonViewModel.linkViewModels);

            foreach (var link in timeLessons) {

                if (link.ToDelete)
                    await DBConnection.DeleteObjects(link.TimeLesson.Id, "time");
            }
        }

        private async Task<List<Time>> GetCallSchedule() {

            var callsSchedule = new List<Time>();
            foreach (var link in TimeLessonViewModel.linkViewModels)
                callsSchedule.Add(link.TimeLesson); // Combining all created Time instances in TimeLesson into a single List

            var times = new List<Time>(await DBConnection.GetTimeAsync());

            callsSchedule.RemoveAll(x => string.IsNullOrWhiteSpace(x.Range) || x.LessonNumber == 0
                                    || times.Contains(x));//Deleting objects with invalid data
            return callsSchedule;
        }

        private void ClearCollection() {

            MondayTimeSchedule.Clear();
            TuesdayTimeSchedule.Clear();
            WednesdayTimeSchedule.Clear();
            ThursdayTimeSchedule.Clear();
            FridayTimeSchedule.Clear();
            SaturdayTimeSchedule.Clear();
        }

        #endregion

    }
}