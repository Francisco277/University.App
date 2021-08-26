using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using University.App.Helpers;
using University.BL.Services.Implements;
using Xamarin.Forms;
using University.BL.DTOs;
namespace University.App.ViewModels.Forms
{
   public class StudentViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<StudentDTO> _Students;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }
         public ObservableCollection<StudentDTO> Students
        {
            get { return this._Students; }
            set { this.SetValue(ref this._Students, value); }
        }

        #endregion

        #region Constructor
        public StudentViewModel()
        {
            this._apiService = new ApiService();
            this.RefreshCommand = new Command(GetStudents);
            this.RefreshCommand.Execute(null);
        }
        #endregion

        #region Methods
        async void GetStudents()
        {
            try
            {
                this.IsRefreshing = true;

                var Connection = await _apiService.CheckConnection();
                if (!Connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Notification", "No Internet Connection", "Cancel");
                    return;
                }

                var responseDTO = await _apiService.RequestAPI<List<StudentDTO>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_STUDENTS, null, ApiService.Method.Get);
                this.Students = new ObservableCollection<StudentDTO>((List<StudentDTO>)responseDTO.Data);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }
        }

        #endregion

        #region Commands
        public Command RefreshCommand { get; set; }
        #endregion
    }
}
