using StudyBug.Models;
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
    public class BreakSettingsView : BindableObject
    {
        public BreakSettingsView() 
        {
        }

        string interval;

        public string Break_Frequency
        {
            get { return interval; }
            set
            {
                interval = value;
                App.ActiveUser.break_frequency = int.Parse(interval);
                OnPropertyChanged();
            }
        }

        string length;

        public string Break_Length
        {
            get { return length; }
            set
            {
                length = value;
                App.ActiveUser.break_frequency = int.Parse(interval);
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
            if (interval != null)
            {
                var route = $"{nameof(Settings)}";
                await Shell.Current.GoToAsync(route);
            }
        }

    }
}
