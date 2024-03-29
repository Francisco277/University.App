﻿using System;
using System.Collections.Generic;
using System.Text;
using University.App.ViewModels.Forms;
using University.App.Views.Forms;
using Xamarin.Forms;

namespace University.App.ViewModels
{
    public class MainViewModel
    {
        public CoursesViewModel Courses { get; set; }
        public StudentViewModel Students { get; set; }
        public OfficeViewModel Offices { get; set; }
        public InstructorsViewModel Instructors { get; set; }
        public CreateCourseViewModel CreateCourse { get; set; }
        public EditCourseViewModel EditCourse { get; set; }
        public EditStudentViewModel EditStudent { get; set; }
  
        public CreateOfficeViewModel CreateOffice { get; set; }
        public CreateStudentViewModel CreateStudent { get; set; }
        public CreateInstructorViewModel CreateInstructor { get; set; }
        public EditOfficeViewModel EditOffice { get; set; }
        public EditInstructorViewModel EditInstructor { get; set; }
        public HomeViewModel Home { get; set; }
        public MainViewModel()
        {
            instance = this;
            this.Home = new HomeViewModel();
            this.CreateCourseCommand = new Command(GoToCreateCourse);
            this.CreateStudentCommand = new Command(GoToCreateStudent);
            this.CreateInstructorCommand = new Command(GoToCreateInstructor);
            this.CreateOfficeCommand = new Command(GoToCreateOffice);
       



        }
        #region Commands
        public Command CreateCourseCommand { get; set; }
        public Command CreateStudentCommand { get; set; }
        public Command CreateInstructorCommand { get; set; }
        public Command CreateOfficeCommand { get; set; }
 
        #endregion

        #region Methods
        async void GoToCreateCourse()
        {
            GetInstance().CreateCourse = new CreateCourseViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateCoursesPage());
        }
        async void GoToCreateStudent()
        {
            GetInstance().CreateStudent = new CreateStudentViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateStudentsPage());
        }

        async void GoToCreateInstructor()
        {
            GetInstance().CreateInstructor = new CreateInstructorViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateInstructorPage());
        }

        async void GoToCreateOffice()
        {
            GetInstance().CreateOffice = new CreateOfficeViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateOfficePage());
        }

        #endregion



        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }
        #endregion
    }
}