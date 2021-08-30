using System;
using System.Collections.Generic;
using System.Text;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
   public class StudentItemViewModel : StudentDTO
    {
        #region Fields 
        private ApiService _apiService;
        #endregion

        #region Methods
        #endregion

        #region Commands
        public Command EditStudentCommand { get; set; }
        public Command DeleteStudentCommand { get; set; }
        #endregion

        #region Constructor
        public StudentItemViewModel()
        {
            this._apiService = new ApiService();
        }
        #endregion
    }
}
