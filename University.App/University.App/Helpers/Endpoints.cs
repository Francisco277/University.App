using System;
using System.Collections.Generic;
using System.Text;

namespace University.App.Helpers
{
    public class Endpoints
    {
        public static string URL_BASE_UNIVERSITY_API { get; set; } = "https://university-api.azurewebsites.net/";
        #region COURSES
        public static string GET_COURSES { get; set; } = "api/Courses/GetCourses/";
        public static string DELETE_COURSES { get; set; } = "api/Courses/";
        public static string POST_COURSES { get; set; } = "api/Courses";
        public static string PUT_COURSES { get; set; } = "api/Courses/";
        #endregion

        #region ESTUDENTS
        public static string GET_STUDENTS { get; set; } = "api/Students/GetStudents/";
        public static string POST_STUDENTS { get; set; } = "api/Students";
        public static string DELETE_STUDENTS { get; set; } = "api/Students/";
        public static string PUT_STUDENTS { get; set; } = "api/Students/";
        #endregion

        #region INSTRUCTORS
        public static string GET_INSTRUCTORS { get; set; } = "api/Instructors/GetInstructors/";
        #endregion

        #region OFFICE
        public static string POST_OFFICES { get; set; } = "api/OfficeAssignments/";
        #endregion
    }
}
