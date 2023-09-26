namespace ScheduleCollegeEditor.Model
{
    public class Answer
    {

        #region Field

        public int Code = 3;
        public string Message;

        #endregion

        #region Property

        public int code {

            get => Code;
            set => Code = value;
        }

        public string message {

            get => Message;
            set => Message = value;
        }

        #endregion

    }
}