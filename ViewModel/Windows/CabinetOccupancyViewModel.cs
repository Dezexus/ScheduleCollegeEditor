using ScheduleCollegeEditor.Data;
using ScheduleCollegeEditor.Model;
using ScheduleCollegeEditor.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ScheduleCollegeEditor.ViewModel
{

    class CabinetOccupancyViewModel : BaseViewModel
    {

        public CabinetOccupancyViewModel(ClassRoom _cabinet) {

            cabinet = _cabinet;
            InicilizationCollection();
        }

        #region Fields

        private List<string> daysWeek = new List<string>() {

            "Понедельник", "Вторник", "Среда",
            "Четверг", "Пятница", "Суббота"
        };
        private List<Schedule> schedule = new List<Schedule>();
        private ClassRoom cabinet;

        #endregion

        #region Propertys

        public ObservableCollection<CabinetLessonCard> MondaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();
        public ObservableCollection<CabinetLessonCard> TuesdaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();
        public ObservableCollection<CabinetLessonCard> WednesdaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();
        public ObservableCollection<CabinetLessonCard> ThursdaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();
        public ObservableCollection<CabinetLessonCard> FridaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();
        public ObservableCollection<CabinetLessonCard> SaturdaySchedule { get; private set; }
            = new ObservableCollection<CabinetLessonCard>();

        public ClassRoom Cabinet {
            get => cabinet;
            set => Set(ref cabinet, value);
        }

        #endregion

        #region Methods

        private async void InicilizationCollection() {

            schedule = await DBConnection.GetScheduleAsync();

            foreach (var day in daysWeek) {

                var cabinetOccupancyDay = CreateCabinetDaySchedule(day);

                switch (day) {

                    case "Понедельник":
                        cabinetOccupancyDay.ForEach(x => MondaySchedule.Add(x));
                        break;
                    case "Вторник":
                        cabinetOccupancyDay.ForEach(x => TuesdaySchedule.Add(x));
                        break;
                    case "Среда":
                        cabinetOccupancyDay.ForEach(x => WednesdaySchedule.Add(x));
                        break;
                    case "Четверг":
                        cabinetOccupancyDay.ForEach(x => ThursdaySchedule.Add(x));
                        break;
                    case "Пятница":
                        cabinetOccupancyDay.ForEach(x => FridaySchedule.Add(x));
                        break;
                    case "Суббота":
                        cabinetOccupancyDay.ForEach(x => SaturdaySchedule.Add(x));
                        break;
                }
            }
        }

        private List<CabinetLessonCard> CreateCabinetDaySchedule(string dayWeek) {

            var daySchedule = schedule.Where(x => x.Cabinet.Id == cabinet.Id && x._WeekDay.DayName == dayWeek).ToList();
            var cabinetOccupancyDay = new List<CabinetLessonCard>();
            var schedules = new List<Schedule>();

            if (daySchedule.Count == 0) return cabinetOccupancyDay;

            for (int i = 1; i <= daySchedule.Max(x => x.Number); i++) {

                var list = daySchedule.Where(x => x.Number == i).ToList();
                if (list.Count == 0) continue;
                Schedule obj = list.First();

                if (list.Count > 1) {

                    string groupList = "";
                    list.ForEach(x => groupList += x._Group.Name + " ");
                    obj._Group.Name = groupList;
                }
                schedules.Add(obj);
            }

            schedules.ForEach(x => cabinetOccupancyDay.Add(new CabinetLessonCard(x)));
            return cabinetOccupancyDay;
        }

        #endregion

    }
}