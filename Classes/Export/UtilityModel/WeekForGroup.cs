using ScheduleCollegeEditor.Model;

namespace ScheduleCollegeEditor.Classes.Export
{
    /// <summary>
    /// Хранит 4 статичных занятия, используется для экспорта
    /// </summary>
    class WeekForGroup
    {
        public Schedule FirstLesson { get; set; } = new Schedule();
        public Schedule SecondLesson { get; set; } = new Schedule();
        public Schedule ThirdLesson { get; set; } = new Schedule();
        public Schedule FourthLesson { get; set; } = new Schedule();
    }
}
