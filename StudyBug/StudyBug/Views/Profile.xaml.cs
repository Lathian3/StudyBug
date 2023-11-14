using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyBug.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void WelcomeAward_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Welcome","Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });            
        }

        private void RibbonOutline_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Start Studying", "Requirements: Create a task to start tracking.", "OK");
            });
        }

        private void Ribbon_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Mindful of the future", "Requirements: Add a reminder to a class", "OK");
            });
        }

        private void FancyRibbon_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Fully Prepared", "Requirements: Created a task to track, reminder and set a weekly goal for the task", "OK");
            });
        }

        private void outline_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("One down", "Requirements: Complete the weekly goal for a task", "OK");
            });
        }

        private void GoldandPurple_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Half way there", "Requirements: Studied for half the total time of all task's goals", "OK");
            });
        }

        private void GoldPurpleWithBanner_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Put in the time", "Requirements: Studied for the total time of all task's goals", "OK");
            });
        }

        private void GoldPurpleWithWings_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Timer Manager", "Requirements: Completed all task's goals for the week", "OK");
            });
        }

        private void trophy_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Part-Time Student", "Requirements: Study 20hrs in a week.\nGreat work!", "OK");
            });
        }

        private void fancyTrophy_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Full-Time Student", "Requirements: Study 40hrs in a week.\nThat's a professional for you, Great work!", "OK");
            });
        }
    }
}