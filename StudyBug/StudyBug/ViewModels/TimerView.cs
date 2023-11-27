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
            //Refresh();
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
        Stopwatch break_countdown = new Stopwatch();
        Stopwatch breakTimer = new Stopwatch();
        Stopwatch snoozeTimer = new Stopwatch();
        TimeSpan ts;
        TimeSpan timeUntilBreak;
        TimeSpan break_length;
        TimeSpan snooze;
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
            await Task.Delay(1000);

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
        bool onbreak = false;
        bool onsnooze = false;
        void OnStart()
        {
        if (onbreak == false)
            {
                if ((App.ActiveUser.breaks_enabled == true) && (onsnooze == false))
                {
                    if (App.ActiveUser.break_frequency == 0 || App.ActiveUser.break_length == 0)
                        App.ActiveUser.breaks_enabled = false;
                    else
                    break_countdown.Start();
                }
            stopwatch.Start();
            if (onsnooze == true) snoozeTimer.Start();
            SetTimer();
            }
        }

        void OnPause()
        {
                stopwatch.Stop();
                if ((App.ActiveUser.breaks_enabled == true) && (onsnooze == false)) break_countdown.Stop();
                if (onsnooze == true) snoozeTimer.Stop();
                aTimer.Enabled = false;
        }

        void OnBreak()
        {
                onbreak = true;
                break_countdown.Reset();
                OnPause();
                breakTimer.Start();
                StudyBreak();
        }

        void OffBreak()
        {
            onbreak = false;
            breakTimer.Reset();
            BreakTimer.Enabled = false;
            OnStart();
        }

        private System.Timers.Timer aTimer = new System.Timers.Timer();

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
            if ((App.ActiveUser.breaks_enabled == true) && (onsnooze == false)) SetBreaks();
            App.ActiveCourse.currentTimeStudied += .5;
            DisplayEllapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
            DisplayProgress = (App.ActiveCourse.currentTimeStudied / (3600 * App.ActiveCourse.Goal)).ToString();
        }

        void SetBreaks()
        {
            timeUntilBreak = break_countdown.Elapsed;
           
            if (timeUntilBreak.Minutes == App.ActiveUser.break_frequency)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    OnPause();
                    bool answer = await App.Current.MainPage.DisplayAlert("Break Time!", "Time to take a break", "OK", "Snooze");
                    if (answer == true) OnBreak();
                    else OnSnooze();
                });
              
            }
        }

         private System.Timers.Timer BreakTimer = new System.Timers.Timer(500);
        
        void StudyBreak()
        {
                BreakTimer.Elapsed += OnBreakEvent;
                BreakTimer.AutoReset = true;
                BreakTimer.Enabled = true;
        }
        
        private void OnBreakEvent(Object source, ElapsedEventArgs e)
        {
           break_length = breakTimer.Elapsed;
            
           if (break_length.Minutes == App.ActiveUser.break_length)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("Study Time!", "Time to get back to work", "OK");
                });
                OffBreak();
            }
        }

        void OnSnooze()
        {
            onsnooze = true;
            OnStart();
            snoozeTimer.Start();
            BreakSnooze();
        }

        void OffSnooze()
        {
            snoozeTimer.Reset();
            SnoozeTimer.Enabled = false;
            onsnooze = false;
            OnBreak();
        }

        private System.Timers.Timer SnoozeTimer = new System.Timers.Timer(500);

        void BreakSnooze()
        {
            SnoozeTimer.Elapsed += OnSnoozeEvent;
            SnoozeTimer.AutoReset = true;
            SnoozeTimer.Enabled = true;
        }

        private void OnSnoozeEvent(Object source, ElapsedEventArgs e)
        {
            snooze = snoozeTimer.Elapsed;
            if(snooze.Minutes == 5)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    OnPause();
                    bool answer = await App.Current.MainPage.DisplayAlert("Break Time!", "Time to take a break", "OK", "Snooze");
                    if (answer == true) OffSnooze();
                    else snoozeTimer.Reset();
                });
            }
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
            await Refresh();
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
