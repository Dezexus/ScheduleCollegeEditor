using Newtonsoft.Json;
using System.Collections.Generic;

namespace ScheduleCollegeEditor.Model
{
    public class Time : BaseModel
    {

        #region Fields

        private int id;
        private string range;
        private int lessonNumber;
        private int weekDayNumber;

        private static List<string> days = new List<string> {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
            "Воскресенье"
        };

        #endregion

        #region Propertys

        [JsonProperty(PropertyName = "id")]
        public int Id {
            get => id;
            set => Set(ref id, value);
        }

        [JsonProperty(PropertyName = "range")]
        public string Range {
            get => range;
            set => Set(ref range, value);
        }

        [JsonProperty(PropertyName = "class_number")]
        public int LessonNumber {
            get => lessonNumber;
            set => Set(ref lessonNumber, value);
        }

        [JsonProperty(PropertyName = "week_day_number")]
        public int WeekDayNumber {
            get => weekDayNumber;
            set => Set(ref weekDayNumber, value);
        }

        #endregion

        #region Method

        public static int GetWeekDayNumberByDayName(string dayName) {

            return days.FindIndex(x => x == dayName) + 1;
        }

        #endregion

        public override string ToString() => LessonNumber.ToString();

    }
}