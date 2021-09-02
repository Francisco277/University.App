using System;
using System.Collections.Generic;
using System.Text;
using University.BL.DTOs;
using University.App.Helpers;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
   public class EditStudentViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private StudentDTO _student;
        private bool _isEnabled;
        private bool _isRunning;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this.SetValue(ref this._isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this._isRunning; }
            set { this.SetValue(ref this._isRunning, value); }
        }

        public StudentDTO Student
        {
            get { return this._student; }
            set { this.SetValue(ref this._student, value); }
        }
        #endregion

        #region Constructor
        public EditStudentViewModel(StudentDTO student)
        {
            this._apiService = new ApiService();
            this.EditStudentCommand = new Command(EditStudent);
            this.IsEnabled = true;
            this.Student = student;
        }
        #endregion

        #region Methods
        async void EditStudent()
        {
            try
            {
                if(string.IsNullOrEmpty(this.Student.LastName) ||
                    string.IsNullOrEmpty(this.Student.EnrollmentDate.ToString())
                    || string.IsNullOrEmpty(this.Student.FirstMidName))
                {
                    await Application.Current.MainPage.DisplayAlert("Notification", "the fields are required", "Cancel");
                    return;
                }
                this.IsEnabled = false;
                this.IsRunning = true;

                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;

                    await Application.Current.MainPage.DisplayAlert("Notification", "No internet Connection", "Cancel");
                    return;
                }

                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<StudentDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.PUT_STUDENTS + this.Student.ID, this.Student, ApiService.Method.Put);
                if (responseDTO.Code < 200 || responseDTO.Code > 200)
                    massage = responseDTO.Message;

                this.IsEnabled = true;
                this.IsRunning = false;

                this.Student.ID = 0;
                this.Student.LastName = string.Empty;
                this.Student.FirstMidName = string.Empty;
                this.Student.EnrollmentDate = Student.EnrollmentDate;

                await Application.Current.MainPage.DisplayAlert("Notification", massage, "Cancel");
            }
            catch (Exception ex)
            {
                this.IsEnabled = true;
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }

        }

    
        #endregion



        #region Commands
        public Command EditStudentCommand { get; set; }
        #endregion
    }
}
