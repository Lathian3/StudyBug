﻿using StudyBug.Views;
using Xamarin.Forms;

namespace StudyBug
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Profile), typeof(Profile));
            Routing.RegisterRoute(nameof(Timer), typeof(Timer));
            Routing.RegisterRoute(nameof(Classes), typeof(Classes));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(ClassNotes), typeof(ClassNotes));
            Routing.RegisterRoute(nameof(EditCourse), typeof(EditCourse));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Registration), typeof(Registration));
            Routing.RegisterRoute(nameof(AddReminders), typeof(AddReminders));
            Routing.RegisterRoute(nameof(BreakSettings), typeof(BreakSettings));
        }

    }
}
