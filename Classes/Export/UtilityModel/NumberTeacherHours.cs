using System.Collections.Generic;

namespace ScheduleCollegeEditor.Classes.Export
{
    /// <summary>
    /// Хранит имя преподавателя и объект информацией по его количеству раб часов
    /// </summary>
    class NumberTeacherHours
    {

        private string teacher;
        private List<CountHourForItem> countHourForItems 
            = new List<CountHourForItem>();

        public string Teacher { 
            get => teacher; 
            set => teacher = value; 
        }

        internal List<CountHourForItem> CountHourForItems { 
            get => countHourForItems; 
            set => countHourForItems = value; 
        }
    }
}
