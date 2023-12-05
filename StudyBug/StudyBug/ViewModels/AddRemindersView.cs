using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class AddRemindersView : BindableObject
    {
        public AddRemindersView() 
        {
        
        }

        string currDesc;
        string currentCourse = App.ActiveCourse.Name;
        DateTime date = DateTime.Now;
        TimeSpan time;
        public DateTime Date { 
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged();
            }
        }

        public string CurrentCourse 
        {
            get { return currentCourse; }
        }

        public string CurrentDescription
        {
            get { return currDesc; }
            set
            {
                currDesc = value;
                OnPropertyChanged();
            }
        }

        private Command save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new Command(PerformSave);
                }

                return save;
            }
        }

        private async void PerformSave()
        {
            if (currDesc != null) 
            {
                App.ActiveReminder.Content = currDesc;
                DateTime dateToSet = date + time;
                App.ActiveReminder.DueDate = dateToSet.ToBinary();
                await DatabaseService.InsertReminder(App.ActiveReminder);
                var route = $"//{nameof(Classes)}";
                await Shell.Current.GoToAsync(route);
            }
        }

        private Command setupCommand;

        public ICommand SetupCommand
        {
            get
            {
                if (setupCommand == null)
                {
                    setupCommand = new Command(Setup);
                }

                return setupCommand;
            }
        }

        private void Setup()
        {
            App.ActiveReminder.CourseId = App.ActiveCourse.Id;
            App.ActiveReminder.CourseName = App.ActiveCourse.Name;
            DateTime now = DateTime.Now;
            App.ActiveReminder.DueDate = now.ToBinary();
        }

    }
}
