using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class ProfileView : BindableObject
    {
        public ICommand GotoSettings { get; }
        public ProfileView()
        {
            GotoSettings = new Command(NavToSettings);
        }

        private async void NavToSettings()
        {
            var route = $"{nameof(Settings)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}