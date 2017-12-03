using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DataTemplates.ViewModels;
using DataTemplates.Pages;
using ZXing.Net.Mobile.Forms;

[assembly:XamlCompilation (XamlCompilationOptions.Compile)]
namespace DataTemplates
{
	public class App : Application
	{
        public static RoomsViewModel RoomsViewModel { get; set; }
        public static RoomsFilterViewModel RoomsFilterViewModel { get; set; }

		public App ()
		{
            RoomsViewModel = new RoomsViewModel();
            RoomsFilterViewModel = new RoomsFilterViewModel();

            MainPage = new NavigationPage(new HomePageCS());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

