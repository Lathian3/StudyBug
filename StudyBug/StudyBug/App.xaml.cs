using MvvmHelpers;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyBug
{
    public partial class App : Application
    {
        public static Course ActiveCourse;

        public static User ActiveUser;

        public static Reminder ActiveReminder;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            ActiveCourse = new Course();
            ActiveUser = new User();
            ActiveReminder = new Reminder();
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
