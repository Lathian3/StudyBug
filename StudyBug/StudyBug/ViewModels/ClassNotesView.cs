using MvvmHelpers.Commands;
using StudyBug.Models;
using StudyBug.Services;
using System.Collections.ObjectModel;
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
