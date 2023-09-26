using Newtonsoft.Json;
using ScheduleCollegeEditor.Classes;
using ScheduleCollegeEditor.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScheduleCollegeEditor.Data
{
    class DBConnection
    {

        #region Fields

        private const string GROUP_SERVLET = "groups";
        private const string SCHEDULE_GROUP_ID_SERVLET = "schedule?group_id=";
        private const string BASKET_GROUP_NAME_SERVLET = "basket?group_name=";
        private const string TEACHER_SERLVET = "teachers";
        private const string CLASSES_SERVLET = "classes";
        private const string CLASSES_GROUP_SERVLET = "classes-for-groups";
        private const string ROOMS_SERVLET = "classrooms";
        private const string TIME_SERVLET = "time-range";
        private const string SCHEDELE_SERVLET = "schedule";
        private const string BASKET_SERVLET = "basket";
        private const string PARAMETR_DAY = "day_name=";
        private const string SCHEDULE_GROUP_SERVLET = "schedule?group_id=";

        private static readonly HttpClient client = new HttpClient();

        public static string Password { get; set; }
        public static string DefaultDomen { get; set; }

        #endregion

        #region Add

        public static async Task<bool> PostRequest(object name, string servletName) {

            if (!Internet.CheckForInternetConnection()) return false;
            
            var request = new HttpRequestMessage(HttpMethod.Post, DefaultDomen + servletName);
            request.Content = new StringContent(JsonConvert.SerializeObject(name).ToString());
            request.Headers.Add("password", Password);
            var response = await client.SendAsync(request);

            string f = JsonConvert.SerializeObject(name).ToString();


            var answer = new Answer();
            if (response.IsSuccessStatusCode)
                answer = await response.Content.ReadAsAsync<Answer>();

            return answer.code == 1;
        }

        public static async Task<bool> AddObjects(object name, string servletName) {
            
            bool IsSuccessStatus = await PostRequest(name, GetServletByName(servletName));
            return IsSuccessStatus;
        }

        public static async Task<bool> AddGroupInSchoolSubject(List<GroupClasses> groupClasses) {

            bool IsSuccessStatus = await PostRequest(groupClasses, CLASSES_GROUP_SERVLET);
            return IsSuccessStatus;
        }

        public static async Task<bool> SaveSchedule(List<ScheduleForSave> schedules) {

            bool IsSuccessStatus = await PostRequest(schedules, SCHEDELE_SERVLET);
            return IsSuccessStatus;
        }

        #endregion

        #region Delete

        public static async Task<bool> DeleteRequest(string parameter, string servletName) {

            if (!Internet.CheckForInternetConnection()) return false;

            var request = new HttpRequestMessage(HttpMethod.Delete, DefaultDomen + servletName + parameter);
            request.Headers.Add("password", Password);
            var response = await client.SendAsync(request);

            var answer = new Answer();
            if (response.IsSuccessStatusCode)
                answer = await response.Content.ReadAsAsync<Answer>();

            return answer.code == 1;
        }

        public static async Task<bool> DeleteAllSchedule () {

            bool IsSuccessStatus = await DeleteRequest("", SCHEDELE_SERVLET);
            return IsSuccessStatus;
        }

        public static async Task<bool> DeleteObjects(int id, string servletName) {

            string parameter = $"?id={id}";
            bool IsSuccessStatus = await DeleteRequest(parameter, GetServletByName(servletName));
            return IsSuccessStatus;
        }

        public static async Task<bool> DeleteGroupSchoolSubject(int groupId, int schoolSubjectId) {

            string parameter = $"?group_id={groupId}&class_id={schoolSubjectId}";
            bool IsSuccessStatus = await DeleteRequest(parameter, CLASSES_GROUP_SERVLET);
            return IsSuccessStatus;
        }

        #endregion
        
        #region Get

        public static async Task<List<Schedule>> GetScheduleAsync() {

            var schedule = new List<Schedule>();
            if (!await CanSendRequest()) return schedule;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + SCHEDELE_SERVLET);

            if (response.IsSuccessStatusCode)
                schedule = await response.Content.ReadAsAsync<List<Schedule>>();

            return schedule;
        }

        public static async Task<List<Schedule>> GetScheduleForGroupByIdAsync(int groupId) {

            var schedule = new List<Schedule>();
            if (!await CanSendRequest()) return schedule;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + SCHEDULE_GROUP_ID_SERVLET + groupId);

            if (response.IsSuccessStatusCode)
                schedule = await response.Content.ReadAsAsync<List<Schedule>>();

            return schedule;
        }

        public static async Task<List<Schedule>> GetTrashForGroupAsync(string group, string dayWeek) {

            var schedule = new List<Schedule>();
            if (!await CanSendRequest()) return schedule;

            HttpResponseMessage response = await client.GetAsync(
                DefaultDomen + BASKET_GROUP_NAME_SERVLET + group + "&" + PARAMETR_DAY + dayWeek);

            if (response.IsSuccessStatusCode)
                schedule = await response.Content.ReadAsAsync<List<Schedule>>();

            return schedule;
        }

        public static async Task<List<Group>> GetGroupsAsync(){

            var groups = new List<Group>();
            if (!await CanSendRequest()) return groups;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + GROUP_SERVLET);
            if (response.IsSuccessStatusCode)
                groups = await response.Content.ReadAsAsync<List<Group>>();
            groups = groups.OrderBy(x => x.Name).ToList();

            return groups;
        }

        public static async Task<List<Class>> GetGroupSchoolSubjectsAsync(int idGroup) {

            var schoolSubjects = new List<Class>();
            if (!await CanSendRequest()) return schoolSubjects;

            schoolSubjects = await GetSchoolSubjectsAsync();
            var groupSchoolSubjects = await GetGroupSchoolSubjects();

            groupSchoolSubjects.RemoveAll(x => x.GroupId != idGroup);
            schoolSubjects = schoolSubjects.Where(x => groupSchoolSubjects.Any(y => y.ClassId == x.Id)).ToList();

            return schoolSubjects.OrderBy(x => x.Name).ToList();
        }

        private static async Task<List<GroupClasses>> GetGroupSchoolSubjects() {

            var groupClasses = new List<GroupClasses>();
            if (!await CanSendRequest()) return groupClasses;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + CLASSES_GROUP_SERVLET);
            if (response.IsSuccessStatusCode)
                groupClasses = await response.Content.ReadAsAsync<List<GroupClasses>>();

            return groupClasses;
        }

        public static async Task<List<Teacher>> GetTeachersAsync() {

            var teachers = new List<Teacher>();
            if (!await CanSendRequest()) return teachers;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + TEACHER_SERLVET);
            if (response.IsSuccessStatusCode)
                teachers = await response.Content.ReadAsAsync<List<Teacher>>();
            teachers = teachers.OrderBy(x => x.FullName).ToList();

            return teachers;
        }

        public static async Task<List<Class>> GetSchoolSubjectsAsync() {

            var schoolSubjects = new List<Class>();
            if (!await CanSendRequest()) return schoolSubjects;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + CLASSES_SERVLET);
            if (response.IsSuccessStatusCode)
                schoolSubjects = await response.Content.ReadAsAsync<List<Class>>();
            schoolSubjects = schoolSubjects.OrderBy(x => x.Name).ToList();

            return schoolSubjects;
        }

        public static async Task<List<ClassRoom>> GetCabinetsAsync() {

            var cabinets = new List<ClassRoom>();
            if (!await CanSendRequest()) return cabinets;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + ROOMS_SERVLET);
            if (response.IsSuccessStatusCode)
                cabinets = await response.Content.ReadAsAsync<List<ClassRoom>>();
            cabinets = cabinets.OrderBy(x => x.Name).ToList();

            return cabinets;
        }

        public async static Task<List<Schedule>> GetCabinetScheduleByIdAsync(int cabinetId) {

            var schedule = await GetScheduleAsync();
            return schedule.Where(x => x.Cabinet.Id == cabinetId).ToList();
        }

        public async static Task<List<Schedule>> GetTeacherScheduleByIdAsync(int teacherId) {

            var schedule = await GetScheduleAsync();
            return schedule.Where(x => x.Teacher.Id == teacherId).ToList();
        }

        public static async Task<List<Time>> GetTimeAsync() {

            var times = new List<Time>();
            if (!await CanSendRequest()) return times;

            HttpResponseMessage response = await client.GetAsync(DefaultDomen + TIME_SERVLET);
            if (response.IsSuccessStatusCode)
                times = await response.Content.ReadAsAsync<List<Time>>();

            return times;
        }

        private static string GetServletByName(string name) {

            switch (name) {

                case "teacher":
                    return TEACHER_SERLVET;
                case "group":
                    return GROUP_SERVLET;
                case "class":
                    return CLASSES_SERVLET;
                case "room":
                    return ROOMS_SERVLET;
                case "time":
                    return TIME_SERVLET;
                default:
                    return null;
            }
        }

        #endregion

        #region Method

        public static async Task<bool> CheckingDomain() {

            bool Successful = false;

            if (Internet.CheckForInternetConnection()) {

                try {

                    HttpResponseMessage response = await client.GetAsync(DefaultDomen);
                    Successful = true;
                } catch {

                    Successful = false;
                }
            }
            return Successful;
        }

        public static async Task<bool> CanSendRequest() {

            return Internet.CheckForInternetConnection() && await CheckingDomain();
        }

        #endregion

    }
}