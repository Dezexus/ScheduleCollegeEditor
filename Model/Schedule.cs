using Newtonsoft.Json;
using ScheduleCollegeEditor.Data;
using System;

namespace ScheduleCollegeEditor.Model
{
    public class Schedule : BaseModel, ICloneable
    {

        #region Field

        private int number;
        private int shift;
        private bool changed;
        private bool toDelete;
        private WeekDay weekDay = new WeekDay();
        private Group group = new Group();
        private Class schoolSubject = new Class();
        private Teacher teacher = new Teacher();
        private ClassRoom cabinet = new ClassRoom();
        private Time time = new Time();

        #endregion

        #region Property

        [JsonProperty(PropertyName = "number")]
        public int Number {
            get => number;
            set => Set(ref number, value);
        }

        [JsonProperty(PropertyName = "shift")]
        public int Shift {
            get => shift;
            set => Set(ref shift, value);
        }

        [JsonProperty(PropertyName = "changed")]
        public bool Changed {
            get => changed;
            set => Set(ref changed, value);
        }

        [JsonProperty(PropertyName = "to_delete")]
        public bool ToDelete {
            get => toDelete;
            set => Set(ref toDelete, value);
        }

        [JsonProperty(PropertyName = "week_day")]
        public WeekDay _WeekDay {
            get => weekDay;
            set => Set(ref weekDay, value);
        }

        [JsonProperty(PropertyName = "group")]
        public Group _Group {
            get => group;
            set => Set(ref group, value);
        }

        [JsonProperty(PropertyName = "class")]
        public Class SchoolSubject {
            get => schoolSubject;
            set => Set(ref schoolSubject, value);
        }

        [JsonProperty(PropertyName = "teacher")]
        public Teacher Teacher {
            get => teacher;
            set => Set(ref teacher, value);
        }

        [JsonProperty(PropertyName = "classroom")]
        public ClassRoom Cabinet {
            get => cabinet;
            set => Set(ref cabinet, value);
        }

        [JsonProperty(PropertyName = "time_range")]
        public Time Time {
            get => time;
            set => Set(ref time, value);
        }

        #endregion

        public static Schedule ScheduleForSaveToSchedule(ScheduleForSave scheduleForSave) {

            var schedule = new Schedule();

            schedule.number = scheduleForSave.Number;
            schedule.shift = scheduleForSave.Shift;
            schedule._WeekDay = WeekDay.GetWeekDayById(scheduleForSave.WeekDayId);
            schedule._Group = GlobalValue.Groups.Find(x => x.Id == scheduleForSave.GroupId);
            schedule.SchoolSubject = GlobalValue.GroupSchoolSubjects.Find(x => x.Id == scheduleForSave.SchoolSubjectId);
            schedule.Teacher = GlobalValue.Teachers.Find(x => x.Id == scheduleForSave.TeacherId);
            schedule.Cabinet = GlobalValue.Cabinets.Find(x => x.Id == scheduleForSave.CabinetId);
            schedule.Time = GlobalValue.Times.Find(x => x.Id == scheduleForSave.TimeId);

            return schedule;
        }

        public object Clone() {

            return this.MemberwiseClone();
        }
    }
}