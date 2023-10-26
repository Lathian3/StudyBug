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

        public static ObservableRangeCollection<Course> CourseList;

        public static ObservableCollection<User> UserList;

        public static async Task RefreshCourses() {
            CourseList.Clear();
            var Courses = await DatabaseService.GetCourse();
            CourseList.AddRange(Courses);
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            ActiveCourse = new Course();
            ActiveUser = new User();
            CourseList = new ObservableRangeCollection<Course>();
            UserList = new ObservableCollection<User>();
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
