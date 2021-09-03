using University.App.Views.Forms;
using Xamarin.Forms;

namespace University.App.ViewModels.Forms
{
    public class HomeViewModel :BaseViewModel

    {
        #region Constructor
        public HomeViewModel()
        {
            this.GetCourseCommand = new Command(GoToCoursesPage);
        }
        #endregion
        #region Commands
        public Command GetCourseCommand { get; set; }
        #endregion

        #region Methods
        async void GoToCoursesPage()
        {
            MainViewModel.GetInstance().Courses = new CoursesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CoursesPage());
        }
    


        async void GoToStudentsPage()
        {
        
        }
        #endregion
    }
}
