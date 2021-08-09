using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IMC.App.Views;
namespace IMC.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new IndexPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
