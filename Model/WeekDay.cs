using Newtonsoft.Json;
using System.Collections.Generic;

namespace ScheduleCollegeEditor.Model
{
    public class WeekDay : BaseModel
    {

        #region Fields

        private int id;
        private string dayName;

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

        #region Property

        [JsonProperty(PropertyName = "id")]
        public int Id { 
            get => id;
            set => Set(ref id, value);
        }

        [JsonProperty(PropertyName = "day_name")]
        public string DayName { 
            get => dayName;
            set => Set(ref dayName, value); 
        }

        #endregion
        
        public static WeekDay GetWeekDayById(int weekDayId) {

            var weekDay = new WeekDay {

                Id = weekDayId,
                DayName = days[weekDayId-1]
            };
            return weekDay;
        }

        public override string ToString() => DayName;

    }
}