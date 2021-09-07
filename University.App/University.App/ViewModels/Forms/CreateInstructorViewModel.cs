using System;
using System.Collections.Generic;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
   public class CreateInstructorViewModel : BaseViewModel
    {
        #region Fields 
        private ApiService _apiService;
        private string _LastName;
        private string _FirstMidName;
        private DateTime _HireDate;
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

        public string Lastname
        {
            get { return this._LastName; }
            set { this.SetValue(ref this._LastName, value); }
        }

        public string Firstmidname
        {
            get { return this._FirstMidName; }
            set { this.SetValue(ref this._FirstMidName, value); }
        }

        public DateTime HireDate
        {
            get { return this._HireDate; }
            set { this.SetValue(ref this._HireDate, value); }
        }

        #endregion

        #region Constructor
        public CreateInstructorViewModel()
        {
            this._apiService = new ApiService();
            this.CreateInstructorCommand = new Command(CreateInstructor);
            this.IsEnabled = true;
            this.HireDate = DateTime.Now;
        }
        #endregion

        #region Methods

        async void CreateInstructor()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Lastname) ||
                    string.IsNullOrEmpty(this.HireDate.ToString())
                    || string.IsNullOrEmpty(this.Firstmidname))
                {
                    await Application.Current.MainPage.DisplayAlert("Notification", "The fields are required", "Cancel");
                    return;
                }
                this.IsEnabled = false;
                this.IsRunning = true;

                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;

                    await Application.Current.MainPage.DisplayAlert("Notification", "No Internet Connection", "Cancel");
                    return;

                }

                var instructorDTO = new InstructorDTO
                {
                    HireDate = this.HireDate,
                    LastName = this.Lastname,
                    FirstMidName = this.Firstmidname

                };

                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<InstructorDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.POST_INTRUCTORS, instructorDTO, ApiService.Method.Post);
                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    massage = responseDTO.Message;

                this.IsEnabled = true;
                this.IsRunning = false;

                this.HireDate = DateTime.Now;
                this.Lastname = string.Empty;
                this.Firstmidname = string.Empty;

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
        public Command CreateInstructorCommand { get; set; }
        #endregion

    }
}
