using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class EditOfficeViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private OfficeDTO _office;
        private bool _isEnabled;
        private bool _isRunning;
        private List<InstructorDTO> _instructors;
        private InstructorDTO _instructorSelected;
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


        public OfficeDTO Office
        {
            get { return this._office; }
            set { this.SetValue(ref this._office, value); }
        }

        public List<InstructorDTO> Instructors
        {
            get { return this._instructors; }
            set { this.SetValue(ref this._instructors, value); }
        }

        public InstructorDTO InstructorSelected
        {
            get { return this._instructorSelected; }
            set { this.SetValue(ref this._instructorSelected, value); }
        }
        #endregion



        #region constructor
        public EditOfficeViewModel(OfficeDTO office)
        {
            this._apiService = new ApiService();
            this.EditOfficeCommand = new Command(EditOffice);
            this.GetInstructorsCommand = new Command(GetInstructors);
            this.GetInstructorsCommand.Execute(null);
            this.IsEnabled = true;

            this.Office = office;
            
        }
        #endregion


        #region Methods
        async void GetInstructors()
        {
            try
            {
                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                    this.IsEnabled = true;
                    this.IsRunning = false;

                    await Application.Current.MainPage.DisplayAlert("Notification", "No internet Connection", "Cancel");
                    return;
                }

                var responseDTO = await _apiService.RequestAPI<List<InstructorDTO>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_INSTRUCTORS, null, ApiService.Method.Get);

                this.Instructors = (List<InstructorDTO>)responseDTO.Data;
                this.InstructorSelected = this.Instructors.FirstOrDefault(x => x.ID == this.Office.InstructorID);
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }
        }

        async void EditOffice()
        {
            try
            {
                if (string.IsNullOrEmpty(this.Office.Location) ||
                    this.InstructorSelected == null)

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
                this.Office.InstructorID = this.InstructorSelected.ID;
                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<OfficeDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.PUT_OFFICES + this.Office.InstructorID, this.Office, ApiService.Method.Put);

                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    massage = responseDTO.Message;

                this.IsEnabled = true;
                this.IsRunning = false;

                this.Office.Location = string.Empty;

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
        public Command EditOfficeCommand { get; set; }
        public Command GetInstructorsCommand { get; set; }
        #endregion
    }
}
