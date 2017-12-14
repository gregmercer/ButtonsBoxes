using Xamarin.Forms;

namespace DataTemplates.Pages
{
	public class HomePageCS : TabbedPage
	{
		public HomePageCS ()
		{
            Title = "GSBgo - Changing the World.";

            Children.Add(new FramesPageCS());
            Children.Add(new ButtonsPageCS());
            //Children.Add(new BoxesPageCS());
            Children.Add(new CustomScanPage());
		}
	}
}
