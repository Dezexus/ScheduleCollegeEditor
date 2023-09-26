using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleCollegeEditor.Classes.Export
{

    /// <summary>
    /// Расписание занятий на день, используется при экспорте
    /// </summary>
    class ScheduleOnDay
    {

        public string WeekDay { get; set; }
        public List<Schedule> scheduleList { get; set; } = new List<Schedule>();

        public void SetSchedule(List<Schedule> schedule, byte maxNumberPair) {

            var schedules = new List<Schedule>();

            for (int i = 1; i <= maxNumberPair; i++) {

                if (schedule.Any(x => x.Number == i)) {

                    var groups = schedule.Where(x => x.Number == i).ToList();
                    Schedule obj = groups.First();

                    if (groups.Count > 1)
                        obj._Group.Name = GroupListAsStr(groups);

                    scheduleList.Add(obj);
                } else {

                    scheduleList.Add(new Schedule { Number = i });
                }
            }
        }

        private string GroupListAsStr(List<Schedule> groups) {

            string groupsStr = "";

            for (int i = 0; i < groups.Count; i++) {

                if (i < groups.Count-1) {

                    groupsStr += groups[i]._Group.Name + ", ";
                } else {

                    groupsStr += groups[i]._Group.Name;
                }
            }
            return groupsStr;
        }
    }
}
