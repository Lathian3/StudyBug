using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyBug.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BreakSettings : ContentPage
	{
		public BreakSettings ()
		{
			InitializeComponent ();
		}
  		 void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        	{

			if (break_switch.IsToggled == true)
			{
				App.ActiveUser.breaks_enabled = true;
			}
			else
			{
				App.ActiveUser.breaks_enabled = false;
			}
        	}
	}
}
