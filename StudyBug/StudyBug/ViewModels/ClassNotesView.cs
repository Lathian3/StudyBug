using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
    }

    

}
