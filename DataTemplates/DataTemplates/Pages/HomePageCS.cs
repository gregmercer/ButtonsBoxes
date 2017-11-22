using Xamarin.Forms;

namespace DataTemplates.Pages
{
	public class HomePageCS : TabbedPage
	{
		public HomePageCS ()
		{
			Children.Add (new ButtonsPageCS ());
            Children.Add(new BoxesPageCS());
		}
	}
}
