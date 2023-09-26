using Newtonsoft.Json;

namespace ScheduleCollegeEditor.Model
{
    public class Teacher : BaseModel
    {

        #region Field

        private int id;
        private string fullName;

        #endregion

        #region Property

        [JsonProperty(PropertyName = "id")]
        public int Id { 
            get => id;
            set => Set(ref id, value);
        }

        [JsonProperty(PropertyName = "fullName")]
        public string FullName { 
            get => fullName;
            set => Set(ref fullName, value); 
        }

        #endregion

        public override string ToString() => FullName;

    }
}