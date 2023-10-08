using StudyBug.Services;
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
            await DatabaseService.GetUser();

            var route = "";

            if(App.ActiveUser == null)
            {
                route = $"{nameof(Registration)}";
                await Shell.Current.GoToAsync(route);
            }
            else {
                route = $"//{nameof(Profile)}";
                await Shell.Current.GoToAsync(route);
            }

        }

    }
}
