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

    public class ClassesView : BindableObject
    {
        public  AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; } 
        public AsyncCommand UpdateCommand { get; }
        public ICommand ClassNotes { get; }
        public ICommand ClassReminder { get; }
        public ObservableRangeCollection<Course> Courses { get; }
        
        public ClassesView() {

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            ClassNotes = new Xamarin.Forms.Command(notesPage);
            ClassReminder = new Xamarin.Forms.Command(remindersPage);
            UpdateCommand = new AsyncCommand(Update);
            Courses = new ObservableRangeCollection<Course>();
            //Refresh();
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

        public bool IsBusy { get; private set; }

        async public void notesPage() 
        {
            var route = $"{nameof(ClassNotes)}";
            await Shell.Current.GoToAsync(route);
        }
        async public void remindersPage()
        {
            if (App.ActiveReminder == null) 
            {
                App.ActiveReminder = new Reminder();
            }
            var route = $"{nameof(AddReminders)}";
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
        }

        
        public async Task Refresh()
        {
            IsBusy = true;

            Courses.Clear();

            var courses = await DatabaseService.GetCourse();

            Courses.AddRange(courses);

            await Task.Delay(1000);

            IsBusy = false;
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

        private void PageDisappearing()
        {
        }

    }

}
