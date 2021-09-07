using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using University.App.Helpers;
using University.BL.DTOs;
using University.BL.Services.Implements;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
   public class OfficeViewModel : BaseViewModel
    {
        #region Fields
        private ApiService _apiService;
        private bool _isRefreshing;
        private ObservableCollection<OfficeItemViewModel> _Offices;
        private List<OfficeItemViewModel> _allOffices;
        private string _filterByOffices;
        #endregion


        #region Properties
        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { this.SetValue(ref this._isRefreshing, value); }
        }
        public ObservableCollection<OfficeItemViewModel> Offices
        {
            get { return this._Offices; }
            set { this.SetValue(ref this._Offices, value); }
        }
        public string FilterByOffices
        {
            get { return this._filterByOffices; }
            set
            {
                this.SetValue(ref this._filterByOffices, value);
                this.GetOfficesByFilter();
            }
        }

        #endregion


        #region Constructor
        public OfficeViewModel()
        {
            this._apiService = new ApiService();
            this.RefreshCommand = new Command(GetOffice);
            this.RefreshCommand.Execute(null);
        }
        #endregion



        #region Methods
        async void GetOffice()
        {
            try
            {
          
                var connection = await _apiService.CheckConnection();
                if (!connection)
                {
                  

                    await Application.Current.MainPage.DisplayAlert("Notification", "No internet Connection", "Cancel");
                    return;
                }

                var massage = "The process is successful";
                var responseDTO = await _apiService.RequestAPI<List<OfficeItemViewModel>>(Endpoints.URL_BASE_UNIVERSITY_API, Endpoints.GET_OFFICES, null, ApiService.Method.Get);

                if (responseDTO.Code < 200 || responseDTO.Code > 299)
                    massage = responseDTO.Message;

                this._allOffices = (List<OfficeItemViewModel>)responseDTO.Data;
                this.Offices = new ObservableCollection<OfficeItemViewModel>(this._allOffices);
                this.IsRefreshing = false;

              

            }
            catch (Exception ex)
            {
           
                await Application.Current.MainPage.DisplayAlert("Notification", ex.Message, "Cancel");
            }

        }

        #endregion
        #region Filter
        void GetOfficesByFilter()
        {
            var offices = this._allOffices;
            if (!string.IsNullOrEmpty(this._filterByOffices))
                offices = offices.Where(x => x.Location.ToLower().Contains(this._filterByOffices)).ToList();

            this.Offices = new ObservableCollection<OfficeItemViewModel>(offices);
        }
        #endregion
        #region Commands
        public Command RefreshCommand { get; set; }


        #endregion
    }
}
