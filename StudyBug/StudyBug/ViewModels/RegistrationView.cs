﻿using MvvmHelpers;
using StudyBug.Models;
using StudyBug.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudyBug.ViewModels
{
    public class RegistrationView : BindableObject
    {
        public ICommand Profile { get; }

        public ObservableRangeCollection<User> Users { get; }
        public RegistrationView() 
        {
            Profile = new Command(gotoProfile);
            App.ActiveUser = new Models.User();
            Users = new ObservableRangeCollection<User>();
        }

        

        string userName = "";
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                App.ActiveUser.Name = userName;
                OnPropertyChanged();
            }
        }

        string goal = "12";
        public string Goal 
        {
            get { return goal; }
            set
            {
                goal = value;
                App.ActiveUser.WeeklyGoal = int.Parse(goal);
                OnPropertyChanged();
            }
        }

    private async void gotoProfile() 
        {
            DateTime loginTime = DateTime.Now;
            App.ActiveUser.lastLoginDate = loginTime.ToBinary();

            loginTime = new DateTime(2023, 11, 05);
            App.ActiveUser.nextResetDate = loginTime.ToBinary();

            App.ActiveUser.Background = ((Color)App.Current.Resources["Theme1Background"]).ToHex();
            App.ActiveUser.Primary = ((Color)App.Current.Resources["Theme1Primary"]).ToHex(); 
            App.ActiveUser.Text = ((Color)App.Current.Resources["Theme1Text"]).ToHex();
            App.ActiveUser.Detail = ((Color)App.Current.Resources["Theme1TextDetail"]).ToHex();

            await DatabaseService.AddUser();
            var route = $"//{nameof(Profile)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
