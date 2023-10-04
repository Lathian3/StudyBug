using MvvmHelpers;
using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    
    public class ClassesView : BindableObject
    {

        public ObservableRangeCollection<Course> Courses { get; set; }
        public  AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Course> RemoveCommand { get; }   
        public AsyncCommand UpdateCommand { get; }
        public ICommand GotoClassNotes { get; }
        
        public ClassesView() {

            Courses = new ObservableRangeCollection<Course>();
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Course>(Remove);
            GotoClassNotes = new Xamarin.Forms.Command(notesPage);
            UpdateCommand = new AsyncCommand(Update);

            Refresh();
        }

        string title = "Class List";
        public string Title
        {
            get => title;
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
                    notesPage();
                    value = null;
                }
                selectedCourse = value;
                OnPropertyChanged();
            }
        }
        
        async public void notesPage() 
        {
            var route = $"{nameof(ClassNotes)}";
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
