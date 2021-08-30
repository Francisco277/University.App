using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using University.App.Helpers;
using University.BL.Services.Implements;
using Xamarin.Forms;
using University.BL.DTOs;
using System.Linq;

namespace University.App.ViewModels.Forms
{
   public class StudentViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<StudentItemViewModel> _Students;
        private List<StudentItemViewModel> _allStudents;
        private string _filterByStudents;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }
         public ObservableCollection<StudentItemViewModel> Students
        {
            get { return this._Students; }
            set { this.SetValue(ref this._Students, value); }
        }
        public string FilterByStudents
        {
            get { return this._filterByStudents; }
            set
            {
                this.SetValue(ref this._filterByStudents, value);
                this.GetStudentsByFilter();
            }
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

                var responseDTO = await _apiService.RequestAPI<List<StudentItemViewModel>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_STUDENTS, null, ApiService.Method.Get);
                this._allStudents = (List<StudentItemViewModel>)responseDTO.Data;
                this.Students = new ObservableCollection<StudentItemViewModel>(this._allStudents);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }
        }

        void GetStudentsByFilter()
        {
            var students = this._allStudents;
            if (!string.IsNullOrEmpty(this.FilterByStudents))
                students = students.Where(x => x.FullName.ToLower().Contains(this.FilterByStudents)).ToList();

            this.Students = new ObservableCollection<StudentItemViewModel>(students);
        }

        #endregion

        #region Commands
        public Command RefreshCommand { get; set; }
        #endregion
    }
}
