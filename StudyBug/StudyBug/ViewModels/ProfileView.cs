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
        public DateTime dateCreated = new DateTime();
        public DateTime currentTime = new DateTime();
        public DateTime sundayTime = new DateTime(2023, 11, 05);

        int numGoalsMet = 0;

        public async Task Refresh()
        {
            await Task.Delay(1000);

            Courses.Clear();
            var course = await DatabaseService.GetCourse();
            Courses.AddRange(course);

            Reminders.Clear();
            var reminders = await DatabaseService.GetAllReminders();
            reminders = reminders.OrderBy(Reminder => DateTime.FromBinary(Reminder.DueDate));
            Reminders.AddRange(reminders);

            await DatabaseService.GetUser();
            double previousTime = totalTimeStudied;
            int userGoal = 0;
            numGoalsMet = 0;

            dateCreated = DateTime.FromBinary(App.ActiveUser.creationDate);
            currentTime = DateTime.Now;

            await ResetTimesAsync();

            foreach (var item in Courses)
            {
                totalTimeStudied += item.currentTimeStudied;
                userGoal += item.Goal;
                if (item.currentTimeStudied >= item.Goal * 60 * 60) {
                    numGoalsMet++;
                }
            }
            totalTimeStudied -= previousTime;
            App.ActiveUser.WeeklyGoal = userGoal;
            Progress = (totalTimeStudied / (3600 * App.ActiveUser.WeeklyGoal)).ToString();
            Greeting = "Hello,\n\t" + App.ActiveUser.Name;
            CheckAchievements();
        }

        public async Task ResetTimesAsync() {
            DateTime now = DateTime.FromBinary(App.ActiveUser.lastLoginDate);
            DateTime reset = DateTime.FromBinary(App.ActiveUser.nextResetDate);
            if (reset < now) 
            {
                while (reset < now)
                    {
                        reset = reset.AddDays(7);
                    }
                App.ActiveUser.nextResetDate = reset.ToBinary();
                foreach (var item in Courses) 
                    {
                    item.currentTimeStudied = 0;
                    }
                await DatabaseService.UpdateUser(App.ActiveUser);
                await DatabaseService.Update(Courses);
                await  Refresh();

            }
        
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

        float WelcomeAwardOpacity = 1;
        float RibbonOutlineOpacity = .5f;
        float RibbonOpacity = .5f;
        float FancyRibbonOpacity = .5f;
        float outlineOpacity = .5f;
        float GoldandPurpleOpacity = .5f;
        float GoldandPurpleWithBannerOpacity = .5f;
        float GoldPurpleWithWingsOpacity = .5f;
        float trophyOpacity = .5f;
        float fancyTrophyOpacity = .5f;

        public float welcomeAwardOpacity { get { return WelcomeAwardOpacity;}
            set 
            {
                WelcomeAwardOpacity = value;
                OnPropertyChanged();
            }
        }
        public float ribbonOutlineOpacity
        {
            get { return RibbonOutlineOpacity; }
            set
            {
                RibbonOutlineOpacity = value;
                OnPropertyChanged();
            }
        }
        public float ribbonOpacity
        {
            get { return RibbonOpacity; }
            set
            {
                RibbonOpacity = value;
                OnPropertyChanged();
            }
        }
        public float fancyRibbonOpacity
        {
            get { return FancyRibbonOpacity; }
            set
            {
                FancyRibbonOpacity = value;
                OnPropertyChanged();
            }
        }
        public float OutlineOpacity
        {
            get { return outlineOpacity; }
            set
            {
                outlineOpacity = value;
                OnPropertyChanged();
            }
        }
        public float goldandPurpleOpacity
        {
            get { return GoldandPurpleOpacity; }
            set
            {
                GoldandPurpleOpacity = value;
                OnPropertyChanged();
            }
        }
        public float goldandPurpleWithBannerOpacity
        {
            get { return GoldandPurpleWithBannerOpacity; }
            set
            {
                GoldandPurpleWithBannerOpacity = value;
                OnPropertyChanged();
            }
        }
        public float goldPurpleWithWingsOpacity
        {
            get { return GoldPurpleWithWingsOpacity; }
            set
            {
                GoldPurpleWithWingsOpacity = value;
                OnPropertyChanged();
            }
        }
        public float TrophyOpacity
        {
            get { return trophyOpacity; }
            set
            {
                trophyOpacity = value;
                OnPropertyChanged();
            }
        }
        public float FancyTrophyOpacity
        {
            get { return fancyTrophyOpacity; }
            set
            {
                fancyTrophyOpacity = value;
                OnPropertyChanged();
            }
        }

        void CheckAchievements()
        {
            if (Courses.Count != 0)
            {
                ribbonOutlineOpacity = 1.0f;
            }
            else {
                ribbonOutlineOpacity = 0.5f;
            }
            if (Reminders.Count != 0) {
                ribbonOpacity = 1.0f;
            }
            else
            {
                ribbonOpacity = 0.5f;
            }
            if (Courses.Count !=0 && Reminders.Count != 0 && App.ActiveUser.WeeklyGoal != 0) {
                fancyRibbonOpacity = 1.0f;
            }
            else
            {
                fancyRibbonOpacity = 0.5f;
            }
            if (numGoalsMet != 0) {
                OutlineOpacity = 1.0f;
            }
            else
            {
                OutlineOpacity = 0.5f;
            }
            if (totalTimeStudied >= 1800 * App.ActiveUser.WeeklyGoal) {
                goldandPurpleOpacity = 1.0f;
            }
            else
            {
                goldandPurpleOpacity = 0.5f;
            }
            if (totalTimeStudied >= 3600 * App.ActiveUser.WeeklyGoal) {
                goldandPurpleWithBannerOpacity = 1.0f;
            }
            else
            {
                goldandPurpleWithBannerOpacity = 0.5f;
            }
            if (numGoalsMet == Courses.Count) {
                goldPurpleWithWingsOpacity = 1.0f;
            }
            else
            {
                goldPurpleWithWingsOpacity = 0.5f;
            }
            if (totalTimeStudied >= (20 * 60 * 60) ) {
                TrophyOpacity = 1.0f;
            }
            else
            {
                TrophyOpacity = 0.5f;
            }
            if (totalTimeStudied >= (40 * 60 * 60) ) {
                FancyTrophyOpacity = 1.0f;
            }
            else
            {
                FancyTrophyOpacity = 0.5f;
            }
        }
    }
}