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
            this.GetStudentsCommand = new Command(GoToStudentsPage);
            this.GetInstructorsCommand = new Command(GoToInstructorsPage);
            this.GetOfficesCommand = new Command(GoToOfficesPage);
        }
        #endregion
        #region Commands
        public Command GetCourseCommand { get; set; }
        public Command GetStudentsCommand { get; set; }
        public Command GetInstructorsCommand { get; set; }
        public Command GetOfficesCommand { get; set; }
    
        #endregion

        #region Methods
        async void GoToCoursesPage()
        {
            MainViewModel.GetInstance().Courses = new CoursesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CoursesPage());
        }
    


        async void GoToStudentsPage()
        {
            MainViewModel.GetInstance().Students = new StudentViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new StudentsPage());
        }

        async void GoToInstructorsPage()
        {
            MainViewModel.GetInstance().Instructors = new InstructorsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new InstructorsPage());
        }

        async void GoToOfficesPage()
        {
            MainViewModel.GetInstance().Offices = new OfficeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new OfficesPage());
        }
        #endregion
    }
}
