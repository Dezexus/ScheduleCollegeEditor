using Newtonsoft.Json;

namespace ScheduleCollegeEditor.Model
{
    public class Class : BaseModel
    {

        #region Field

        private int id;
        private string name;

        #endregion

        #region Property

        [JsonProperty(PropertyName = "id")]
        public int Id {
            get => id;
            set => Set(ref id, value);
        }

        [JsonProperty(PropertyName = "class_name")]
        public string Name {
            get => name;
            set => Set(ref name, value);
        }

        #endregion

        public override string ToString() => Name;
    }
}