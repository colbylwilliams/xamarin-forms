using Xamarin.Forms;

namespace PickerOverModal
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage (new MainPage ());
		}
	}
}
