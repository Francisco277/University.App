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
   public class StudentItemViewModel : StudentDTO
    {
        #region Fields 
        private ApiService _apiService;
        #endregion

        #region Methods
        async void EditStudent()
        {
            MainViewModel.GetInstance().EditStudent = new EditStudentViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditStudentsPage());
        }
        async void DeleteStudent()
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
                var responseDTO = await _apiService.RequestAPI<StudentDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.DELETE_STUDENTS + this.ID, null, ApiService.Method.Delete);

                if (responseDTO.Code < 200 || responseDTO.Code > 299)
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
        public Command EditStudentCommand { get; set; }
        public Command DeleteStudentCommand { get; set; }
        #endregion

        #region Constructor
        public StudentItemViewModel()
        {
            this._apiService = new ApiService();
            this.DeleteStudentCommand = new Command(DeleteStudent);
            this.EditStudentCommand = new Command(EditStudent);
        }
        #endregion
    }
}
