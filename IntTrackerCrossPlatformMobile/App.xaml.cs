using IntTrackerCrossPlatformMobile.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace IntTrackerCrossPlatformMobile
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();

            //busyPage = new BusyPage();
            //loginPage = new LoginPage();
            //MainPage = loginPage;

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }

           // MainPage = new MainPage();
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
