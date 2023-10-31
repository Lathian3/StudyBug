using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class ProfileView : BindableObject
    {
        public ICommand GotoSettings { get; }
        public AsyncCommand RefreshCommand { get; }

        public ObservableRangeCollection<Course> Courses { get; }
        public ObservableRangeCollection<Reminder> Reminders { get; }
        public ProfileView()
        {
            GotoSettings = new Xamarin.Forms.Command(NavToSettings);
            Courses = new ObservableRangeCollection<Course>();
            Reminders = new ObservableRangeCollection<Reminder>();
            RefreshCommand = new AsyncCommand(Refresh);
        }

        public double totalTimeStudied = 0;
        
        public async Task Refresh()
        {
            Courses.Clear();
            var course = await DatabaseService.GetCourse();
            Courses.AddRange(course);

            Reminders.Clear();
            var reminders = await DatabaseService.GetAllReminders();
            Reminders.AddRange(reminders);

            await DatabaseService.GetUser();
            double previousTime = totalTimeStudied;
            foreach (var item in Courses)
            {
                totalTimeStudied += item.currentTimeStudied;
            }
            totalTimeStudied -= previousTime;
            Progress = (totalTimeStudied / (3600 * App.ActiveUser.WeeklyGoal)).ToString();
            Greeting = "Hello,\n\t" + App.ActiveUser.Name;
        }


        string progress;

        public string Progress
        {
            get { return progress; }
            set 
            {
                if (progress == value) 
                { return; }
                progress = value;
                OnPropertyChanged();
            }
        }
        public string greeting = "Hello,\n\t" + App.ActiveUser.Name;
        public string Greeting
        {
            get { return greeting; }
            set { greeting = value; OnPropertyChanged(); }
        }
        private async void NavToSettings()
        {
            var route = $"{nameof(Settings)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}