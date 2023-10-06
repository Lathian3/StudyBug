using StudyBug.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    class LoginView : BindableObject
    {
        public ICommand LoginCommand { get; }
        public LoginView() {
            LoginCommand = new Command(Login);
        }

        public async void Login()
        {
            var route = $"//{nameof(Profile)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}
