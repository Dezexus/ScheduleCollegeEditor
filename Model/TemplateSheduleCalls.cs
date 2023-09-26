namespace ScheduleCollegeEditor.Model
{
    public class TemplateSheduleCalls
    {

        #region Property

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        public override string ToString() => Name;

    }
}