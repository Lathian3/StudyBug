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

        double interval = App.ActiveUser.break_frequency;

        public int Break_Frequency
        {
            get { return (int)Math.Floor(interval); }
            set
            {
                interval = value;
                App.ActiveUser.break_frequency = (int)Math.Floor(interval);
                OnPropertyChanged();
            }
        }
        private Command setFrequency;

        public ICommand SetFrequency
        {
            get
            {
                if (setFrequency == null)
                {
                    setFrequency = new Command(PerformSetFrequency);
                }

                return setFrequency;
            }
        }

        private void PerformSetFrequency()
        {
            App.ActiveUser.break_frequency = Break_Frequency; 
        }

        double length = App.ActiveUser.break_length;

        public int Break_Length
        {
            get { return (int)Math.Floor(length); }
            set
            {
                length = value;
                App.ActiveUser.break_length = (int)Math.Floor(length);
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
                await DatabaseService.UpdateUser(App.ActiveUser);
                var route = $"{nameof(Settings)}";
                await Shell.Current.GoToAsync(route);            
        }

        private Command setDuration;

        public ICommand SetDuration
        {
            get
            {
                if (setDuration == null)
                {
                    setDuration = new Command(PerformSetDuration);
                }

                return setDuration;
            }
        }

        private void PerformSetDuration()
        {
            App.ActiveUser.break_length = Break_Length;
        }

    }
}
