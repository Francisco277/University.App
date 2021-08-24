using System;
using System.Collections.Generic;
using System.Text;
using University.App.ViewModels.Forms;
namespace University.App.ViewModels
{
    public class MainViewModel
    {
        public CoursesViewModel Courses { get; set; }

        public MainViewModel()
        {
            this.Courses = new CoursesViewModel();
        }
    }
}
