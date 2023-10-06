using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    class SettingsView : BindableObject
    {
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Course> RemoveCommand { get; }
        public AsyncCommand UpdateCommand { get; }
        public ICommand GotoEditCourse { get; }
        public ICommand GotoProfile { get; }
        public SettingsView() 
        {
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Course>(Remove);
            GotoEditCourse = new Xamarin.Forms.Command(EditPage);
            GotoProfile = new Xamarin.Forms.Command(ProfilePage);
            UpdateCommand = new AsyncCommand(Update);

            Courses = new ObservableRangeCollection<Course>();
            LoadCourses();
        }
        public ObservableRangeCollection<Course> Courses { get; set; }

        async Task LoadCourses()
        {
            Courses.Clear();
            var courses = await CourseService.GetCourse();
            Courses.AddRange(courses);
        }

        Course selectedCourse;
        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                if (value != null)
                {
                    App.ActiveCourse = value;
                    value = null;
                }
                selectedCourse = value;
                OnPropertyChanged();
            }
        }
        async public void EditPage()
        {
            var route = $"/{nameof(EditCourse)}";
            await Shell.Current.GoToAsync(route);
        }

        async public void ProfilePage()
        {
            var route = $"//{nameof(Profile)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name");
            await CourseService.AddCourse(name);
            await Refresh();
        }

        async Task Update()
        {
            await CourseService.Update(Courses);
            await Refresh();
        }

        async Task Remove(Course course)
        {
            await CourseService.RemoveCourse(course.Id);
            await Refresh();
        }

        public bool IsBusy;
        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(1000);

            Courses.Clear();

            var courses = await CourseService.GetCourse();

            Courses.AddRange(courses);

            IsBusy = false;
        }
    }

    
}
