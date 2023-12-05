using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudyBug.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Classes : ContentPage
	{
		public Classes ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
			AddNote.IsEnabled = true;
			AddReminder.IsEnabled = true;
			((ListView)sender).SelectedItem = null;
        }

    
    }
}