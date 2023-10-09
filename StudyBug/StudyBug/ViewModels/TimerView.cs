using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class TimerView : BindableObject
    {
        public ObservableRangeCollection<Course> Courses { get; set; }
        public ICommand SetActiveCourse { get; }
        public ICommand CourseSelected => new Xamarin.Forms.Command<Course>((item) =>
        {   
            App.ActiveCourse = item;
            stopwatch.Reset();
        });
        public AsyncCommand Save { get; }
        public TimerView()
        {
            StartTimer = new Xamarin.Forms.Command(OnStart);
            StopTimer = new Xamarin.Forms.Command(OnPause);
            Courses = new ObservableRangeCollection<Course>();
            Save = new AsyncCommand(Update);
            LoadCourses();
        }
        string progress = (App.ActiveCourse.currentTimeStudied / (3600 * App.ActiveCourse.Goal)).ToString();
        public string DisplayProgress
        {
            get => progress;
            set
            {
                if (value == progress)
                {
                    return;
                }
                progress = value;
                OnPropertyChanged();
            }
        }
        async Task LoadCourses()
        {
            Courses.Clear();
            var courses = await DatabaseService.GetCourse();
            Courses.AddRange(courses);
        }

        string EllapsedTime = "Start Studying";
        Stopwatch stopwatch = new Stopwatch();
        TimeSpan ts;

        string title = "Study Timer";
        public string Title
        {
            get => title;
        }
        async Task Update()
        {
            await DatabaseService.Update(Courses);
            await Refresh();
        }
        async Task Refresh()
        {
            Courses.Clear();

            var courses = await DatabaseService.GetCourse();

            Courses.AddRange(courses);
        }
        public string DisplayEllapsedTime
        {
            get => EllapsedTime;
            set
            {
                if (value == EllapsedTime)
                {
                    return;
                }
                EllapsedTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartTimer { get; }
        public ICommand StopTimer { get; }

        void OnStart()
        {
            stopwatch.Start();
            SetTimer();
        }

        void OnPause()
        { 
            stopwatch.Stop();
            aTimer.Enabled = false;
        }

        private System.Timers.Timer aTimer;

        private void SetTimer()
        {
            // Create a timer with a .5 sec interval.
            aTimer = new System.Timers.Timer(500);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ts = stopwatch.Elapsed;
            App.ActiveCourse.currentTimeStudied += .5;
            DisplayEllapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
            DisplayProgress = (App.ActiveCourse.currentTimeStudied / (3600 * App.ActiveCourse.Goal)).ToString();
        }

    }
}
