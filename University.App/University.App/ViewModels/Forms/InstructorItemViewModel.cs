using System;
using System.Collections.Generic;
using System.Text;
using University.App.Helpers;
using University.App.Views.Forms;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
   public class InstructorItemViewModel : InstructorDTO

    {
        #region Fields
        private ApiService _apiService;

        #endregion

        #region Methods

        async void EditInstructor()
        {
            MainViewModel.GetInstance().EditInstructor = new EditInstructorViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditInstructorPage());
        }

        async void DeleteInstructor()
        {
            try
            {
                var answer = await Application.Current.MainPage.DisplayAlert("Notification", "Delete Confirm", "YES", "NO");
                if (!answer)
                    return;

                var connection = await _apiService.CheckConnection();
                if (!connection)
                {


                    await Application.Current.MainPage.DisplayAlert("Notification", "No internet Connection", "Cancel");
                    return;
                }


                var message = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<InstructorDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.DELETE_INSTRUCTORS + this.ID, null, ApiService.Method.Delete);

                if (responseDTO.Code < 200 || responseDTO.Code > 200)
                    message = responseDTO.Message;


                await Application.Current.MainPage.DisplayAlert("Notification", message, "Cancel");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }

        }
        #endregion

        #region Commands
        public Command EditInstructorCommand { get; set; }
        public Command DeleteInstructorCommand { get; set; }
        #endregion

        #region Constructor
        public InstructorItemViewModel()
        {
            this._apiService = new ApiService();
            this.DeleteInstructorCommand = new Command(DeleteInstructor);
            this.EditInstructorCommand = new Command(EditInstructor);
        }
        #endregion
    }
}

    
