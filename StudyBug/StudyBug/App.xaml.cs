using StudyBug.Models;
using StudyBug.Services;
using Xamarin.Forms;

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
