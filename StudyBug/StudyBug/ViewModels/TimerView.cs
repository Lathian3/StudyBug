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

        public ICommand CourseSelected => new Xamarin.Forms.Command<Course>((item) =>
        {   
            App.ActiveCourse = item;
            stopwatch.Reset();
        });
        public AsyncCommand Save { get; }
        public AsyncCommand RefreshCommand { get; }
        public TimerView()
        {
            StartTimer = new Xamarin.Forms.Command(OnStart);
            StopTimer = new Xamarin.Forms.Command(OnPause);
            Courses = new ObservableRangeCollection<Course>();
            Save = new AsyncCommand(Update);
            RefreshCommand = new AsyncCommand(Refresh);
            Refresh();
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
        public async Task Refresh()
        {
            var courses = await DatabaseService.GetCourse();
            Courses.Clear();
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

        private Xamarin.Forms.Command pageAppearingCommand;

        public ICommand PageAppearingCommand
        {
            get
            {
                if (pageAppearingCommand == null)
                {
                    pageAppearingCommand = new Xamarin.Forms.Command(PageAppearing);
                }

                return pageAppearingCommand;
            }
        }

        private async void PageAppearing()
        {
            Courses.Clear();
        }

        private Xamarin.Forms.Command pageDisappearingCommand;

        public ICommand PageDisappearingCommand
        {
            get
            {
                if (pageDisappearingCommand == null)
                {
                    pageDisappearingCommand = new Xamarin.Forms.Command(PageDisappearing);
                }

                return pageDisappearingCommand;
            }
        }

        private async void PageDisappearing()
        {
            await DatabaseService.Update(Courses);
        }
    }
}
