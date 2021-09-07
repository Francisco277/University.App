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
    public class OfficeItemViewModel : OfficeDTO
    {
        #region Fields
        private ApiService _apiService;
        #endregion


        #region Methods

        async void EditOffice()
        {
            MainViewModel.GetInstance().EditOffice = new EditOfficeViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditOfficesPage());
        }

        async void DeleteOffice()
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
                var responseDTO = await _apiService.RequestAPI<OfficeDTO>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.DELETE_OFFICES + this.InstructorID, null, ApiService.Method.Delete);

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
        public Command EditOfficeCommand { get; set; }
        public Command DeleteOfficeCommand { get; set; }
        #endregion


        #region Constructor
        public OfficeItemViewModel()
        {
            this._apiService = new ApiService();
            this.DeleteOfficeCommand = new Command(DeleteOffice);
            this.EditOfficeCommand = new Command(EditOffice);
        }
        #endregion
    }

}

