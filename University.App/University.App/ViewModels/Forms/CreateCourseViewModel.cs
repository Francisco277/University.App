using System;
using System.Collections.Generic;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class CreateCourseViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private int _courseID;
        private string _tittle;
        private int _credits;
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

        public int CourseID
        {
            get { return this._courseID; }
            set { this.SetValue(ref this._courseID, value); }
        }

        public string Tittle
        {
            get { return this._tittle; }
            set { this.SetValue(ref this._tittle, value); }
        }

        public int Credits
        {
            get { return this._credits; }
            set { this.SetValue(ref this._credits, value); }
        }

        #endregion

        #region constructor
        public CreateCourseViewModel()
        {
            this._apiService = new ApiService();
            this.CreateCourseCommand = new Command(CreateCourse);
            this.IsEnabled = true;

        }
        #endregion

        #region Methods
        async void CreateCourse()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Tittle) ||
                    this.Credits == 0 ||
                    this.CourseID == 0)
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

                var courseDTO = new CourseDTO
                {
                    CourseID = this.CourseID,
                    Title = this.Tittle,
                    Credits = this.Credits
                };
                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<CourseDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.POST_COURSES, courseDTO, ApiService.Method.Post);
                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    massage = responseDTO.Message;
                this.IsEnabled = true;
                this.IsRunning = false;

                this.CourseID = this.Credits = 0;
                this.Tittle = string.Empty;


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
        public Command CreateCourseCommand { get; set; }
        #endregion
    }
}
