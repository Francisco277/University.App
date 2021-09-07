using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class EditInstructorViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private InstructorDTO _instructor;
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

        public InstructorDTO Instructor
        {
            get { return this._instructor; }
            set { this.SetValue(ref this._instructor, value); }
        }
        #endregion


        #region Constructor
        public EditInstructorViewModel(InstructorDTO instructor)
        {
            this._apiService = new ApiService();
            this.EditInstructorCommand = new Command(EditInstructor);
            this.IsEnabled = true;
            this.Instructor = instructor;
        }
        #endregion


        #region Methods
        async void EditInstructor()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Instructor.LastName) ||
                    string.IsNullOrEmpty(this.Instructor.HireDate.ToString())
                    || string.IsNullOrEmpty(this.Instructor.FirstMidName))
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
                var responseDTO = await _apiService.RequestAPI<InstructorDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.PUT_INSTRUCTORS + this.Instructor.ID, this.Instructor, ApiService.Method.Put);
                if (responseDTO.Code < 200 || responseDTO.Code > 200)
                    massage = responseDTO.Message;

                this.IsEnabled = true;
                this.IsRunning = false;

                this.Instructor.ID = 0;
                this.Instructor.LastName = string.Empty;
                this.Instructor.FirstMidName = string.Empty;
                this.Instructor.HireDate = Instructor.HireDate;

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
        public Command EditInstructorCommand { get; set; }
        #endregion
    }
}




