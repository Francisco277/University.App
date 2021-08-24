using System;
using System.Collections.Generic;
using System.Text;

namespace University.App.Helpers
{
    public class Endpoints
    {
        public static string URL_BASE_UNIVERSITY_API { get; set; } = "https://university-api.azurewebsites.net/";
        public static string GET_COURSES { get; set; } = "api/Courses/GetCourses/";
    }
}
