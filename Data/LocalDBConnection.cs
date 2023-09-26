using ScheduleCollegeEditor.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace ScheduleCollegeEditor.Data
{
    class LocalDBConnection
    {

        #region Field

        private static readonly string PathDB = "data\\AppDB.db";
        private static readonly string ConnectionString = $"Data Source={PathDB};VERSION=3;";
        private static SQLiteConnection m_dbConnection;

        #endregion

        #region Create

        private static void Connection() {

            if (!File.Exists(PathDB)) CreateDB();

            var command = new SQLiteConnection(ConnectionString);
            m_dbConnection = command;
            m_dbConnection.Open();
            SQLiteCommand comd = m_dbConnection.CreateCommand();
            comd.CommandText = "PRAGMA foreign_keys=on;";
            comd.ExecuteNonQuery();
        }

        private static void CreateDB() {

            SQLiteConnection.CreateFile(PathDB);
            Connection();
            CreateTable();
        }

        private static void CreateTable() {

            SQLiteCommand command = m_dbConnection.CreateCommand();

            command.CommandText = "CREATE TABLE Template (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "name VARCHAR(40) NOT NULL DEFAULT NULL);";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE Call (" +
                "idTemplate REFERENCES Template (id) ON DELETE CASCADE," +
                "range VARCHAR(50) NOT NULL," +
                "lessonNumber INT(1) NOT NULL," +
                "weekDayNumber INT(1) NOT NULL);";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE ChangedLesson (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                "number INT(1) NOT NULL," +
                "shift INT(1) NOT NULL," +
                "weekDayId INT NOT NULL," +
                "groupId INT NOT NULL," +
                "schoolSubjectId INT NOT NULL," +
                "teacherId INT NOT NULL," +
                "cabinetId INT NOT NULL," +
                "timeId INT NOT NULL);";
            command.ExecuteNonQuery();
        }

        #endregion

        #region Add

        public static void AddTemplate(string name, List<Time> times) {

            SQLiteCommand command = GetCommand();

            command.CommandText = $"INSERT INTO Template (name) VALUES ('{name}'); " +
                                  "SELECT Max(id) FROM Template;";
            int idTemplate = Convert.ToInt32(command.ExecuteScalar());
            AddCallsInTamplate(idTemplate, times);
        }

        public static void AddChangedLesson(ScheduleForSave schedule) {

            if (ExistChangedLesson(schedule)) {

                UpdateChangedLesson(schedule);
                return;
            }

            SQLiteCommand command = GetCommand();

            command.CommandText = $"INSERT INTO ChangedLesson (" +
                $"number," +
                $"shift," +
                $"weekDayId," +
                $"groupId," +
                $"schoolSubjectId," +
                $"teacherId," +
                $"cabinetId," +
                $"timeId) " +
                $"VALUES (" +
                $"{schedule.Number}," +
                $"{schedule.Shift}," +
                $"{schedule.WeekDayId}," +
                $"{schedule.GroupId}," +
                $"{schedule.SchoolSubjectId}," +
                $"{schedule.TeacherId}," +
                $"{schedule.CabinetId}," +
                $"{schedule.TimeId});";
            command.ExecuteNonQuery();
        }

        private static void AddCallsInTamplate(int idTemplate, List<Time> times) {

            SQLiteCommand command = GetCommand();
            string sqlQuery = "";

            foreach (var time in times) {

                sqlQuery += $"INSERT INTO Call (idTemplate, range, lessonNumber, weekDayNumber) " +
                    $"VALUES (" +
                    $"{idTemplate}, " +
                    $"'{time.Range}', " +
                    $"{time.LessonNumber}, " +
                    $"{time.WeekDayNumber});";
            }

            command.CommandText = sqlQuery;
            command.ExecuteNonQuery();
        }

        #endregion

        #region Update

        private static void UpdateChangedLesson(ScheduleForSave schedule) {

            SQLiteCommand command = GetCommand();

            command.CommandText = $"UPDATE ChangedLesson SET " +
                $"number = {schedule.Number}," +
                $"shift = {schedule.Shift}," +
                $"weekDayId = {schedule.WeekDayId}," +
                $"groupId = {schedule.GroupId}," +
                $"schoolSubjectId = {schedule.SchoolSubjectId}," +
                $"teacherId = {schedule.TeacherId}," +
                $"timeId = {schedule.TimeId}," +
                $"cabinetId = {schedule.CabinetId} " +
                $"WHERE number = {schedule.Number} " +
                    $"AND weekDayId = {schedule.WeekDayId} " +
                    $"AND groupId = {schedule.GroupId};";
            command.ExecuteNonQuery();
        }

        #endregion

        #region Delete

        public static void DeleteTemplate(int idTemplate) {

            SQLiteCommand command = GetCommand();

            command.CommandText =$"DELETE FROM Template WHERE id = {idTemplate};";
            command.ExecuteNonQuery();
        }

        public static void DeleteChangedLesson(ScheduleForSave schedule) {

            SQLiteCommand command = GetCommand();

            command.CommandText = $"DELETE FROM ChangedLesson " +
                $"WHERE number = {schedule.Number} " +
                $"AND weekDayId = {schedule.WeekDayId} " +
                $"AND groupId = {schedule.GroupId};";
            command.ExecuteNonQuery();
        }

        #endregion

        #region Get

        public static List<Time> GetCallsBIdTemplate(int idTemplate) {

            SQLiteCommand command = GetCommand();
            SQLiteDataReader reader;
            List<Time> times = new List<Time>();

            command.CommandText = $"SELECT * FROM Call WHERE idTemplate = {idTemplate};";
            reader = command.ExecuteReader();

            while (reader.Read()) {

                var time = new Time {

                    Range = reader.GetString(reader.GetOrdinal("range")),
                    LessonNumber = reader.GetInt32(reader.GetOrdinal("lessonNumber")),
                    WeekDayNumber = reader.GetInt32(reader.GetOrdinal("weekDayNumber"))
                };
                times.Add(time);
            }
            return times;
        }

        public static List<TemplateSheduleCalls> GetTemplateList() {

            SQLiteCommand command = GetCommand();
            SQLiteDataReader reader;
            var templates = new List<TemplateSheduleCalls>();

            command.CommandText = $"SELECT * FROM Template;";
            reader = command.ExecuteReader();

            while (reader.Read()) {

                var template = new TemplateSheduleCalls {

                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name"))
                };
                templates.Add(template);
            }

            return templates;
        }

        public static ScheduleForSave GetChangedLesson(ScheduleForSave schedule) {

            SQLiteCommand command = GetCommand();
            SQLiteDataReader reader;
            var scheduleForSave = new ScheduleForSave();

            command.CommandText = $"SELECT * FROM ChangedLesson " +
                $"WHERE number = {schedule.Number} " +
                $"AND weekDayId = {schedule.WeekDayId} " +
                $"AND groupId = {schedule.GroupId};";
            reader = command.ExecuteReader();

            while (reader.Read()) {

                scheduleForSave = new ScheduleForSave {

                    Number = reader.GetInt32(reader.GetOrdinal("number")),
                    Shift = reader.GetInt32(reader.GetOrdinal("shift")),
                    WeekDayId = reader.GetInt32(reader.GetOrdinal("weekDayId")),
                    GroupId = reader.GetInt32(reader.GetOrdinal("groupId")),
                    SchoolSubjectId = reader.GetInt32(reader.GetOrdinal("schoolSubjectId")),
                    TeacherId = reader.GetInt32(reader.GetOrdinal("teacherId")),
                    CabinetId = reader.GetInt32(reader.GetOrdinal("cabinetId")),
                    TimeId = reader.GetInt32(reader.GetOrdinal("timeId"))
                };
            }

            return scheduleForSave;
        }

        public static bool ExistChangedLesson(ScheduleForSave schedule) {

            SQLiteCommand command = GetCommand();

            command.CommandText = $"SELECT * FROM ChangedLesson " +
                $"WHERE number = {schedule.Number} " +
                $"AND weekDayId = {schedule.WeekDayId} " +
                $"AND groupId = {schedule.GroupId};";
            return command.ExecuteReader().HasRows;
        }

        #endregion
            
        #region Method

        private static SQLiteConnection GetConnection() {

            if (m_dbConnection == null) Connection();
            return m_dbConnection;
        }

        private static SQLiteCommand GetCommand() {

            var command = GetConnection().CreateCommand();
            return command;
        }

        #endregion

    }
}