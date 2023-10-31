using StudyBug.Services;
using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
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
                await DatabaseService.InsertReminder(currDesc, App.ActiveCourse);
                var route = $"//{nameof(Classes)}";
                await Shell.Current.GoToAsync(route);
            }
        }


    }
}
