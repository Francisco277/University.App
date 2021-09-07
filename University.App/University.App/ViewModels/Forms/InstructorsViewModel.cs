using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using University.App.Helpers;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class InstructorsViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<InstructorItemViewModel> _instructors;
        private List<InstructorItemViewModel> _allInstructors;
        private string _filterByInstructors;
        #endregion




        #region Properties

        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }

        public ObservableCollection<InstructorItemViewModel> Instructors
        {
            get { return this._instructors; }
            set { this.SetValue(ref this._instructors, value); }
        }

        public string FilterByInstructors
        {
            get { return this._filterByInstructors; }
            set
            {
                this.SetValue(ref this._filterByInstructors, value);
                this.GetInstructorsByFilter();
            }

        }

        #endregion


        #region constructor
        public InstructorsViewModel()
        {
            this._apiService = new ApiService();
            this.RefreshCommand = new Command(GetInstructors);
            this.RefreshCommand.Execute(null);
        }
        #endregion


        #region Methods
        async void GetInstructors()
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

                var responseDTO = await _apiService.RequestAPI<List<InstructorItemViewModel>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_INSTRUCTORS, null, ApiService.Method.Get);

                this._allInstructors = (List<InstructorItemViewModel>)responseDTO.Data;
                this.Instructors = new ObservableCollection<InstructorItemViewModel>(this._allInstructors);
                this.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }

        }


        void GetInstructorsByFilter()
        {
            var instructors = this._allInstructors;
            if (!string.IsNullOrEmpty(this.FilterByInstructors))
                instructors = instructors.Where(x => x.FullName.ToLower().Contains(this.FilterByInstructors)).ToList();

            this.Instructors = new ObservableCollection<InstructorItemViewModel>(instructors);


        }
        #endregion

        #region Commands
        public Command RefreshCommand { get; set; }
        #endregion
    }


    }

