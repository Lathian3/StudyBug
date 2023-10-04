using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class EditCourseView : BindableObject
    {
        public ICommand GotoSettings { get; }
        public EditCourseView()
        {
            GotoSettings = new Xamarin.Forms.Command(SettingsPage);
        }
        async public void SettingsPage()
        {
            var route = $"{nameof(Settings)}";
            await Shell.Current.GoToAsync(route);
        }

        string currCourse = App.ActiveCourse.Name;
        int currGoal = App.ActiveCourse.Goal;

        public string CurrentCourse
        {
            get { return currCourse; }
            set
            {
                currCourse = value;
                App.ActiveCourse.Name = currCourse;
                OnPropertyChanged();
            }
        }

        public int CurrentGoal
        {
            get { return currGoal; }
            set
            {
                currGoal = value;
                App.ActiveCourse.Goal = currGoal;
                OnPropertyChanged();
            }
        }
    }
    
}
