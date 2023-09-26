using Newtonsoft.Json;
using ScheduleCollegeEditor.Data;

namespace ScheduleCollegeEditor.Model
{
    public class ScheduleForSave : BaseModel
    {

        #region Field

        private int number;
        private int shift;
        private bool changed;
        private bool toDelete;
        private int weekDayId;
        private int groupId;
        private int schoolSubjectId;
        private int teacherId;
        private int cabinetId;
        private int timeId;

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

        [JsonProperty(PropertyName = "week_day_id")]
        public int WeekDayId {
            get => weekDayId;
            set => Set(ref weekDayId, value);
        }

        [JsonProperty(PropertyName = "group_id")]
        public int GroupId {
            get => groupId;
            set => Set(ref groupId, value);
        }

        [JsonProperty(PropertyName = "class_id")]
        public int SchoolSubjectId {
            get => schoolSubjectId;
            set => Set(ref schoolSubjectId, value);
        }

        [JsonProperty(PropertyName = "teacher_id")]
        public int TeacherId {
            get => teacherId;
            set => Set(ref teacherId, value);
        }

        [JsonProperty(PropertyName = "classroom_id")]
        public int CabinetId {
            get => cabinetId;
            set => Set(ref cabinetId, value);
        }

        [JsonProperty(PropertyName = "time_range_id")]
        public int TimeId {
            get => timeId;
            set => Set(ref timeId, value);
        }

        #endregion

        public static ScheduleForSave ScheduleToScheduleForSave(Schedule schedule) {

            var scheduleForSave = new ScheduleForSave {

                Number = schedule.Time.LessonNumber,
                Shift = GlobalValue.Shift,
                Changed = schedule.Changed,
                ToDelete = schedule.ToDelete,
                WeekDayId = schedule._WeekDay.Id,
                GroupId = schedule._Group.Id,
                SchoolSubjectId = schedule.SchoolSubject.Id,
                TeacherId = schedule.Teacher.Id,
                CabinetId = schedule.Cabinet.Id,
                TimeId = schedule.Time.Id,
            };

            return scheduleForSave;
        }
    }
}