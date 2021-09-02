﻿using System;
using System.Collections.Generic;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class CreateStudentViewModel : BaseViewModel
    {
        #region Fields 
        private ApiService _apiService;
        private string _LastName;
        private string _FirstMidName;
        private DateTime _EnrollmentDate;
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

        public DateTime Enrollmentdate
        {
            get { return this._EnrollmentDate; }
            set { this.SetValue(ref this._EnrollmentDate, value); }
        }

        #endregion

        #region Constructor
        public CreateStudentViewModel()
        {
            this._apiService = new ApiService();
            this.CreateStudentCommand = new Command(CreateStudent);
            this.IsEnabled = true;
            this.Enrollmentdate = DateTime.Now;
        }
        #endregion

        #region Methods

        async void CreateStudent()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Lastname) ||
                    string.IsNullOrEmpty(this.Enrollmentdate.ToString())
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

                var studenDTO = new StudentDTO
                {
                    EnrollmentDate = this.Enrollmentdate,
                    LastName = this.Lastname,
                    FirstMidName = this.Firstmidname

                };

                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<StudentDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.POST_STUDENTS, studenDTO, ApiService.Method.Post);
                if (responseDTO.Code < 200 || responseDTO.Code > 200)
                    massage = responseDTO.Message;

                this.IsEnabled = true;
                this.IsRunning = false;

                this.Enrollmentdate = DateTime.Now;
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
        public Command CreateStudentCommand { get; set; }
        #endregion
    }
}
