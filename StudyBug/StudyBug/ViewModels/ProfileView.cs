﻿using MvvmHelpers;
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
            int userGoal = 0;

            dateCreated = DateTime.FromBinary(App.ActiveUser.creationDate);
            currentTime = DateTime.Now;

            await ResetTimesAsync();

            foreach (var item in Courses)
            {
                totalTimeStudied += item.currentTimeStudied;
                userGoal += item.Goal;
            }
            totalTimeStudied -= previousTime;
            App.ActiveUser.WeeklyGoal = userGoal;
            Progress = (totalTimeStudied / (3600 * App.ActiveUser.WeeklyGoal)).ToString();
            Greeting = "Hello,\n\t" + App.ActiveUser.Name;
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

    }
}