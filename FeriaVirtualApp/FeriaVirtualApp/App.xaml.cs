using FeriaVirtualApp.Helpers;
using FeriaVirtualApp.Models;
using FeriaVirtualApp.ViewModels;
using FeriaVirtualApp.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeriaVirtualApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = LoggedIn() ? (Page) new MainPage() : new NavigationPage(new LoginPage());

            Messaging.Subscribe<LoginViewModel>(this, Constants.LoginDone, OnLoginDone);
        }

        private void OnLoginDone(LoginViewModel sender)
        {
            MainPage = new MainPage();
        }

        private bool LoggedIn()
        {
            var loggedIn = Preferences.Get(Config.UserLogged, false, Config.SharedName);
            var userType = Preferences.Get(Config.UserType, null, Config.SharedName);

            return loggedIn && userType != null;
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
