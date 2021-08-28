using System.Collections.ObjectModel;
using University.BL.DTOs;
using University.BL.Services.Implements;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using University.App.Helpers;
using System.Linq;

namespace University.App.ViewModels.Forms
{
    public class CoursesViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<CourseDTO> _Courses;
        private List<CourseDTO> _allCourses;
        private string _filter;
        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }

        public ObservableCollection<CourseDTO> Courses
        {
            get { return this._Courses; }
            set { this.SetValue(ref this._Courses, value); }
        }

        public string Filter
        {
            get { return this._filter; }
            set
            {
                this.SetValue(ref this._filter, value);
                this.GetCoursesByFilter();
            }
            
        }

        #endregion

        #region constructor
        public CoursesViewModel()
        {
            this._apiService = new ApiService();
            this.RefreshCommand = new Command(GetCourses);
            this.RefreshCommand.Execute(null);
        }
        #endregion

        #region Methods
        async void GetCourses()
        {
            try
            {
                this.IsRefreshing = true;

                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                    this.IsRefreshing = false;
                    await Application.Current.MainPage.DisplayAlert("Notification", "No internet Connection", "Cancel");
                    return;
                }

                var responseDTO = await _apiService.RequestAPI<List<CourseDTO>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_COURSES, null, ApiService.Method.Get);

                this._allCourses = (List<CourseDTO>)responseDTO.Data;
                this.Courses = new ObservableCollection<CourseDTO>((List<CourseDTO>)this._allCourses);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }

        }


        void GetCoursesByFilter()
        {
            var courses = this._allCourses;
            if (!string.IsNullOrEmpty(this.Filter))
                courses = courses.Where(x => x.Title.ToLower().Contains(this.Filter)).ToList();

            this.Courses = new ObservableCollection<CourseDTO>(courses);

            courses = courses.Where(x => x.Title.ToLower().Contains(this.Filter) || x.Credits.ToString().Contains(this.Filter)).ToList();
        }
        #endregion

        #region Commands
        public Command RefreshCommand { get; set; }


        #endregion




    }

}

