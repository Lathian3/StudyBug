using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    class ClassNotesView : BindableObject
    {
        public ClassNotesView() 
        {
            
        }

        string currDesc = App.ActiveCourse.Description;

        public string CurrentDescription
        {
            get { return currDesc; }
            set
            {
                currDesc = value;
                App.ActiveCourse.Description = currDesc;
                OnPropertyChanged();
            }
        }

        private AsyncCommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new AsyncCommand(PerformSaveAsync);
                }

                return save;
            }
        }

        private async Task PerformSaveAsync()
        {
            ObservableCollection<Course> courses = new ObservableCollection<Course>
            {
                App.ActiveCourse
            };
            await DatabaseService.Update(courses);
        }
    }

    

}
