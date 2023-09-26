using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ScheduleCollegeEditor.ViewModel
{
    class FillGroupScheduleViewModel : BaseViewModel
    {

        public FillGroupScheduleViewModel() {

            GlobalValue.FillGroupScheduleViewModelRef = this;
            InitializationCommand();
            InitializationCollection();

            dispatcherTimer.Tick += new EventHandler(CheckingChangesSelectedGroup);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Start();
        }

        #region Field


        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private readonly LoadingAnimation loadAnimation = new LoadingAnimation();
        private bool loadAnimationVisible = false;
        private Group selectedGroupLastChangedValue;
        private Group selectedGroup;
        private byte shift = 1;
        private bool shiftBoxEnable = false;

        #endregion

        #region Property

        public ObservableCollection<Group> GroupsList { get; private set; }
            = new ObservableCollection<Group>();

        public ObservableCollection<WeekDayScheduleEditor> WeekDayScheduleEditorList { get; private set; }
            = new ObservableCollection<WeekDayScheduleEditor>();
        public ObservableCollection<InfoIntersectionLessonCard> InfoIntersectionLessonCardList { get; private set; }
            = new ObservableCollection<InfoIntersectionLessonCard>();

        public LoadingAnimation LoadAnimation {
            get => loadAnimation;
        }

        public bool LoadAnimationVisible {
            get => loadAnimationVisible;
            set => Set(ref loadAnimationVisible, value);
        }

        public Group SelectedGroup {
            get => selectedGroup;
            set {
                if (value != null) {

                    ShiftBoxEnable = true;
                    Set(ref selectedGroup, value);
                } else {
                    ShiftBoxEnable = false;
                }
            }
        }

        public byte Shift {
            get => shift;
            set {
                Set(ref shift, value);
                GlobalValue.Shift = value;
            } 
        }

        public bool ShiftBoxEnable {
            get => shiftBoxEnable;
            set => Set(ref shiftBoxEnable, value);
        }

        #endregion

        #region Command

        public ICommand FillScheduleCommand { get; private set;}
        public ICommand SaveScheduleCommand { get; private set; }
        public ICommand ReplacePairsWithPracticeCommand { get; private set; }

        #endregion

        #region MethodCommand
        
        private async void FillSchedule(object obj) {

            LoadAnimationVisible = true;
            GlobalValue.GroupSchoolSubjects = await DBConnection.GetGroupSchoolSubjectsAsync(selectedGroup.Id);
            GlobalValue.Times = await DBConnection.GetTimeAsync();
            GlobalValue.Cabinets = await DBConnection.GetCabinetsAsync();
            GlobalValue.Teachers = await DBConnection.GetTeachersAsync();
            GlobalValue.Groups = await DBConnection.GetGroupsAsync();
            GlobalValue.Schedules = await DBConnection.GetScheduleAsync();
            var schedule = GlobalValue.Schedules.Find(x => x._Group.Id == selectedGroup.Id) ?? new Schedule();
            Shift = (byte)(schedule.Shift == 0 ? 1 : schedule.Shift);

            LessonCardWithEditViewModel.links.Clear();
            WeekDayScheduleEditorList.Clear();
            foreach (var weekDay in GlobalValue.WeekDays)
                WeekDayScheduleEditorList.Add(new WeekDayScheduleEditor(selectedGroup, weekDay));
            LoadAnimationVisible = false;
        }
        
        private async void SaveSchedule(object obj) {

            LoadAnimationVisible = true;
            var schedule = GetScheduleForSave();
            schedule = RemoveAllNotChangedAndEmptyObject(schedule);
            
            if (schedule.Count == 0) {

                MainWindowViewModel.VMLink.Notification("Изменений в расписании не обнаружено", "warning");
                return;
            }

            var answer = await Task.Run(() => DBConnection.SaveSchedule(schedule));
            if (answer) {

                WeekDayScheduleEditorList.Clear();
                SaveChangedLesson();
                DeleteRestoredLessonFromLDB();
                MainWindowViewModel.VMLink.Notification("Расписание сохранено");
                FillSchedule(new object());
            } else {

                MainWindowViewModel.VMLink.Notification("Не удалось сохранить расписание", "fault");
            }
            LoadAnimationVisible = false;
        }
        
        private async void ReplacePairsWithPractice(object obj) {
            
            if (!(MessageBox.Show("Заменить все пары на практику?", "Замена пар на практику", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.No)) {

                LoadAnimationVisible = true;
                if (await CheckingForTheNecessaryData()) {

                    var cabinets = await DBConnection.GetCabinetsAsync();
                    var teachers = await DBConnection.GetTeachersAsync();
                    var schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();
                    var times = await DBConnection.GetTimeAsync();
                    var schedule = new List<ScheduleForSave>();
                    byte numberLesson = 1;

                    foreach (var lesson in GetScheduleForSave()) {

                        numberLesson = numberLesson > 4 ? (byte)1 : numberLesson;

                        lesson.SchoolSubjectId = schoolSubjects.Find(x => x.Name == "Практика").Id;
                        lesson.TeacherId = teachers.Find(x => x.FullName == "Руководитель практики").Id;
                        lesson.Number = numberLesson;
                        lesson.TimeId = times.Find(x => x.LessonNumber == numberLesson && x.WeekDayNumber == lesson.WeekDayId).Id;
                        lesson.CabinetId = cabinets.Find(x => x.Name == "0").Id;
                        lesson.Changed = true;
                        schedule.Add(lesson);
                        numberLesson++;
                    }

                    var answer = await Task.Run(() => DBConnection.SaveSchedule(schedule));
                    if (answer) {

                        SaveChangedLesson();
                        DeleteRestoredLessonFromLDB();
                        MainWindowViewModel.VMLink.Notification("Расписание сохранено");
                        FillSchedule(new object());
                    } else {

                        MainWindowViewModel.VMLink.Notification("Не удалось сохранить расписание", "fault");
                    }
                }
                LoadAnimationVisible = false;
            }
        }
        
        private bool CanFillSchedule(object arg) {

            return (arg as Group) != null;
        }
        
        private bool CanSaveSchedule(object arg) {

            if (WeekDayScheduleEditorList .Count != 0)
                return true;
            return false;
        }
        
        private bool CanReplacePairsWithPractice(object arg) {

            if (WeekDayScheduleEditorList.Count != 0)
                return true;
            return false;
        }

        #endregion

        #region Method
        
        private void InitializationCommand() {

            ReplacePairsWithPracticeCommand = new DelegateCommand(ReplacePairsWithPractice, CanReplacePairsWithPractice);
            FillScheduleCommand = new DelegateCommand(FillSchedule, CanFillSchedule);
            SaveScheduleCommand = new DelegateCommand(SaveSchedule, CanSaveSchedule);
        }
        
        private async void InitializationCollection() {

            var groups = await DBConnection.GetGroupsAsync();
            groups.ForEach(x => GroupsList.Add(x));
        }
        
        private List<ScheduleForSave> GetScheduleForSave() {
             
            var schedules = new List<ScheduleForSave>();

            foreach (var link in LessonCardWithEditViewModel.links)
                schedules.Add(link.GetScheduleForSave());

            return schedules;
        } 
        
        private List<ScheduleForSave> RemoveAllNotChangedAndEmptyObject(List<ScheduleForSave> schedule) {

            schedule.RemoveAll(x => x.SchoolSubjectId == 0);

            var scheduleForGroupWithoutChanged = GlobalValue.Schedules.FindAll(x => x._Group.Id == selectedGroup.Id).ToList();

            schedule.RemoveAll(x => scheduleForGroupWithoutChanged.Any(y => x.Number == y.Number && x.WeekDayId == y._WeekDay.Id &&
                x.TeacherId == y.Teacher.Id && x.CabinetId == y.Cabinet.Id && x.Changed == y.Changed && x.ToDelete == y.ToDelete 
                && x.GroupId == y._Group.Id && x.SchoolSubjectId == y.SchoolSubject.Id));
            return schedule;
        }
        
        private void SaveChangedLesson() {

            foreach (var link in LessonCardWithEditViewModel.links)
                if (link.LocalChangedStatus)
                    LocalDBConnection.AddChangedLesson(link.LessonNotChanged);
        }
        
        private void DeleteRestoredLessonFromLDB() {

            foreach (var link in LessonCardWithEditViewModel.links)
                if (link.DeletedChangedStatus)
                    LocalDBConnection.DeleteChangedLesson(link.GetScheduleForSave());
        }
        
        private void CheckingChangesSelectedGroup(object sender, EventArgs e) {

            if (selectedGroup != selectedGroupLastChangedValue && selectedGroup != null) {

                selectedGroupLastChangedValue = selectedGroup;
                FillSchedule(new object());
            }
        }
        
        private async Task<bool> CheckingForTheNecessaryData() {

            bool answer;
            var answers = new List<bool>();
            var cabinets = await DBConnection.GetCabinetsAsync();
            var teachers = await DBConnection.GetTeachersAsync();
            var schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();
            Class className = schoolSubjects.Find(x => x.Name == "Практика");
            int class_id = 0;
            if (className != null) class_id = className.Id;


            if (!cabinets.Any(x => x.Name == "0")) {

                answer = await Task.Run(() => DBConnection.AddObjects(
                    new ClassRoom { Name = "0" }, "room"));

                answers.Add(answer);
            }

            if (!teachers.Any(x => x.FullName == "Руководитель практики")) {

                answer = await Task.Run(() => DBConnection.AddObjects(
                    new Teacher { FullName = "Руководитель практики" }, "teacher"));

                answers.Add(answer);
            }

            if (!schoolSubjects.Any(x => x.Name == "Практика")) {

                answer = await Task.Run(() => DBConnection.AddObjects(
                    new Class { Name = "Практика" }, "class"));

                answers.Add(answer);
                schoolSubjects = await DBConnection.GetSchoolSubjectsAsync();
                class_id = schoolSubjects.Find(x => x.Name == "Практика").Id;
            }

            var groupSchoolSubjects = await DBConnection.GetGroupSchoolSubjectsAsync(selectedGroup.Id);

            if (!groupSchoolSubjects.Any(x => x.Id == class_id)) {

                var groupClassesList = new List<GroupClasses>();

                var groupClasses = new GroupClasses {
                    ClassId = class_id,
                    GroupId = selectedGroup.Id
                };
                groupClassesList.Add(groupClasses);
                answer = await Task.Run(() => DBConnection.AddGroupInSchoolSubject(groupClassesList));
                answers.Add(answer);
            }

            if (answers.Any(x => x == false))
                return false;
            return true;
        }

        #endregion

    }
}