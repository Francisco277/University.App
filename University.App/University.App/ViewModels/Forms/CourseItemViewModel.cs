using System;
using System.Collections.Generic;
using System.Text;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class CourseItemViewModel : CourseDTO
    {
        #region Fields
        private ApiService _apiService;

        #endregion

        #region Methods

        #endregion

        #region Commands
        public Command EditCourseCommand { get; set; }
        public Command DeleteCourseCommand { get; set; }
        #endregion

        #region Constructor
        public CourseItemViewModel()
        {
            this._apiService = new ApiService();
        }
        #endregion
    }
}
