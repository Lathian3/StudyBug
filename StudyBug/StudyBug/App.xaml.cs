using MvvmHelpers;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyBug
{
    public partial class App : Application
    {
        public static Course ActiveCourse;

        public static User ActiveUser;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            ActiveCourse = new Course();
            ActiveUser = new User();
        }

        protected override void OnStart()
        {
            DatabaseService.Init();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
        

    }
}
