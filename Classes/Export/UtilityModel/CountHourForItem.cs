namespace ScheduleCollegeEditor.Classes.Export
{

    /// <summary>
    /// Хранит название предмета, группу и количество часов по данному предмету
    /// </summary>
    class CountHourForItem
    {

        private string name;
        private string group;
        private int hour;

        public string Name { 
            get => name; 
            set => name = value; 
        }

        public string Group {
            get => group;
            set => group = value;
        }

        public int Hour { 
            get => hour; 
            set => hour = value; 
        }
    }
}
