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
        public  AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; } 
        public AsyncCommand UpdateCommand { get; }
        public ICommand ClassNotes { get; }
        public ObservableRangeCollection<Course> Courses { get; }
        
        public ClassesView() {

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            ClassNotes = new Xamarin.Forms.Command(notesPage);
            UpdateCommand = new AsyncCommand(Update);
            Courses = new ObservableRangeCollection<Course>();
            
            Refresh();
        }

        string title = "Class List";

        public string Title
        {
            get => title;
        }
        public Course SelectedCourse
        {
            get { return App.ActiveCourse; } 
            set 
            {
                if (value != null) 
                {
                    App.ActiveCourse = value;
                }
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
            await DatabaseService.AddCourse(name);
            await Refresh();
        }

        async Task Update()
        {
           await DatabaseService.Update(Courses);
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
