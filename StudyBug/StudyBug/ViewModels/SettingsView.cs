﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    class SettingsView : BindableObject
    {
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand RemoveCommand { get; }
        public AsyncCommand UpdateCommand { get; }
        public ICommand GotoProfile { get; }

        public ICommand GotoBreakSettings { get; }
        public ICommand CourseSelected => new Xamarin.Forms.Command<Course>((item) =>
        {
            App.ActiveCourse = item;
        });

        public SettingsView()
        {
            RefreshCommand = new AsyncCommand(LoadData);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand(Remove);
            GotoProfile = new Xamarin.Forms.Command(ProfilePage);
            UpdateCommand = new AsyncCommand(Update);
            Courses = new ObservableRangeCollection<Course>();
            Users = new ObservableRangeCollection<User>();
            GotoBreakSettings = new Xamarin.Forms.Command(BreakPage);
        }
        public ObservableRangeCollection<Course> Courses { get; set; }
        public ObservableRangeCollection<User> Users { get; set; }

        async Task LoadData()
        {
            Courses.Clear();
            var courses = await DatabaseService.GetCourse();
            Courses.AddRange(courses);
            Users.Clear();
            var users = await DatabaseService.GetUsers();
            Users.AddRange(users);
        }
        
        async public void ProfilePage()
        {
            await Update();
            var route = $"//{nameof(Views.Profile)}";
            await Shell.Current.GoToAsync(route);
        }
        async public void BreakPage()
        {
            await Update();
            var route = $"{nameof(BreakSettings)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name", "OK", "");
            if (!(string.IsNullOrWhiteSpace(name)))
            {
                await DatabaseService.AddCourse(name);
                await Refresh();
            }
        }

        async Task Update()
        {
            await DatabaseService.Update(Courses);
            await DatabaseService.UpdateUser(App.ActiveUser);
            await Refresh();
        }

        async Task Remove()
        {
            await DatabaseService.RemoveCourse(App.ActiveCourse.Id);
            await Refresh();
        }

        public bool IsBusy;
        async Task Refresh()
        {
            IsBusy = false;

            await Task.Delay(1000);

            Courses.Clear();

            var courses = await DatabaseService.GetCourse();

            Courses.AddRange(courses);

            IsBusy = true;
        }
    }

    
}
