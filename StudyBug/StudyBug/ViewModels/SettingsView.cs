using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StudyBug.ViewModels
{
    class SettingsView : BindableObject
    {
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Course> RemoveCommand { get; }
        public AsyncCommand UpdateCommand { get; }
        public ICommand GotoProfile { get; }
        public ICommand CourseSelected => new Xamarin.Forms.Command<Course>((item) =>
        {
            App.ActiveCourse = item;
        });

        public SettingsView()
        {
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Course>(Remove);
            GotoProfile = new Xamarin.Forms.Command(ProfilePage);
            UpdateCommand = new AsyncCommand(Update);
            Courses = new ObservableRangeCollection<Course>();
            Users = new ObservableRangeCollection<User>();
            
            LoadData();
        }
        public ObservableRangeCollection<Course> Courses { get; set; }
        public ObservableRangeCollection<User> Users { get; set; }

        async Task LoadData()
        {
            Courses.Clear();
            var courses = await DatabaseService.GetCourse();
            Courses.AddRange(courses);
            var users = await DatabaseService.GetUsers();
            Users.AddRange(users);
        }

        async public void ProfilePage()
        {
            await Update();
            var route = $"//{nameof(Views.Profile)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            await DatabaseService.AddCourse(name);
            await Refresh();
        }

        async Task Update()
        {
            await DatabaseService.Update(Courses);
            await Refresh();
        }

        async Task Remove(Course course)
        {
            await DatabaseService.RemoveCourse(course.Id);
            await Refresh();
        }

        public bool IsBusy;
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(1000);

            Courses.Clear();

            var courses = await DatabaseService.GetCourse();

            Courses.AddRange(courses);

            IsBusy = false;
        }
    }

    
}
