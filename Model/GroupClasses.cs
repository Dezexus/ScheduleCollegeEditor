using Newtonsoft.Json;

namespace ScheduleCollegeEditor.Model
{
    public class GroupClasses : BaseModel
    {

        #region Field

        private int groupId;
        private int classId;

        #endregion

        #region Propert

        [JsonProperty(PropertyName = "group_id")]
        public int GroupId {
            get => groupId;
            set => Set(ref groupId, value);
        }

        [JsonProperty(PropertyName = "class_id")]
        public int ClassId {
            get => classId;
            set => Set(ref classId, value);
        }

        #endregion

    }
}