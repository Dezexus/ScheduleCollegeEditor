using ScheduleCollegeEditor.Data;
using XMLFileSettings;

namespace ScheduleCollegeEditor.Classes
{
    class Setting
    {

        public static void LoadSetting() {

            Props props = new Props();

            props.ReadXml();
            Appearance.SetTheme = (Appearance.Themes)props.Fields.selectModeTheme;
            GlobalValue.NumberLessons = props.Fields.numberLessons;
            DBConnection.Password = props.Fields.password;
            DBConnection.DefaultDomen = props.Fields.defaultDomen;
            new Appearance();
        }

        public static void SaveSetting() {

            Props props = new Props();

            props.Fields.selectModeTheme = (int)Appearance.SelectedTheme;
            props.Fields.numberLessons = GlobalValue.NumberLessons;
            props.Fields.password = DBConnection.Password;
            props.Fields.defaultDomen = DBConnection.DefaultDomen;
            props.WriteXml();
        }

    }
}