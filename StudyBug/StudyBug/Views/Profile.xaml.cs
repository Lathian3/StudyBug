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
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void Ribbon_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void FancyRibbon_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void outline_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void GoldandPurple_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void GoldPurpleWithBanner_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
            });
        }

        private void GoldPurpleWithWings_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage.DisplayAlert("Achievement", "Requirements: Create a user profile.\nWelcome to Study Bug!", "OK");
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