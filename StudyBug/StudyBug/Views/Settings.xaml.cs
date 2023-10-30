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
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
			InitializeComponent ();
		}

        private void Theme1Button_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
			if (e.Value == true) {
				Theme2Button.IsChecked = false; 
				Theme3Button.IsChecked = false;
				Theme4Button.IsChecked = false;

                App.Current.Resources["Primary"] = App.Current.Resources["Theme1Background"];
				App.Current.Resources["PageBackgroundColor"] = App.Current.Resources["Theme1Background"];
                App.Current.Resources["CourseCardColor"] = App.Current.Resources["Theme1Primary"];
                App.Current.Resources["mainButtonColor"] = App.Current.Resources["Theme1Primary"];
                App.Current.Resources["ButtonTextColor"] = App.Current.Resources["Theme1Text"];
                App.Current.Resources["TextColor"] = App.Current.Resources["Theme1Text"];
                App.Current.Resources["TabBarColor"] = App.Current.Resources["Theme1Background"];
                App.Current.Resources["ProgressBarColor"] = App.Current.Resources["Theme1TextDetail"];
            }
        }

        private void Theme2Button_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                Theme1Button.IsChecked = false;
                Theme3Button.IsChecked = false;
                Theme4Button.IsChecked = false;

                App.Current.Resources["Primary"] = App.Current.Resources["Theme2Background"];
                App.Current.Resources["PageBackgroundColor"] = App.Current.Resources["Theme2Background"];
                App.Current.Resources["CourseCardColor"] = App.Current.Resources["Theme2Primary"];
                App.Current.Resources["mainButtonColor"] = App.Current.Resources["Theme2Primary"];
                App.Current.Resources["ButtonTextColor"] = App.Current.Resources["Theme2Text"];
                App.Current.Resources["TextColor"] = App.Current.Resources["Theme2Text"];
                App.Current.Resources["TabBarColor"] = App.Current.Resources["Theme2Background"];
                App.Current.Resources["ProgressBarColor"] = App.Current.Resources["Theme2TextDetail"];
            }
        }

        private void Theme3Button_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                Theme1Button.IsChecked = false;
                Theme2Button.IsChecked = false;
                Theme4Button.IsChecked = false;

                App.Current.Resources["Primary"] = App.Current.Resources["Theme3Background"];
                App.Current.Resources["PageBackgroundColor"] = App.Current.Resources["Theme3Background"];
                App.Current.Resources["CourseCardColor"] = App.Current.Resources["Theme3Primary"];
                App.Current.Resources["mainButtonColor"] = App.Current.Resources["Theme3Primary"];
                App.Current.Resources["ButtonTextColor"] = App.Current.Resources["Theme3Text"];
                App.Current.Resources["TextColor"] = App.Current.Resources["Theme3Text"];
                App.Current.Resources["TabBarColor"] = App.Current.Resources["Theme3Background"];
                App.Current.Resources["ProgressBarColor"] = App.Current.Resources["Theme3TextDetail"];
            }
        }

        private void Theme4Button_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                Theme1Button.IsChecked = false;
                Theme2Button.IsChecked = false;
                Theme3Button.IsChecked = false;

                App.Current.Resources["Primary"] = App.Current.Resources["Theme4Background"];
                App.Current.Resources["PageBackgroundColor"] = App.Current.Resources["Theme4Background"];
                App.Current.Resources["CourseCardColor"] = App.Current.Resources["Theme4Primary"];
                App.Current.Resources["mainButtonColor"] = App.Current.Resources["Theme4Primary"];
                App.Current.Resources["ButtonTextColor"] = App.Current.Resources["Theme4Text"];
                App.Current.Resources["TextColor"] = App.Current.Resources["Theme4Text"];
                App.Current.Resources["TabBarColor"] = App.Current.Resources["Theme4Background"];
                App.Current.Resources["ProgressBarColor"] = App.Current.Resources["Theme4TextDetail"];
            }
        }
    }
}