using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SlideOverKit;

namespace DataTemplates.Pages
{
    public partial class RoomsFilterPage : SlideMenuView
    {
        public RoomsFilterPage()
        {
            InitializeComponent ();

            // You must set IsFullScreen in this case, 
            // otherwise you need to set HeightRequest, 
            // just like the QuickInnerMenu sample
            this.IsFullScreen = true;

            // You must set WidthRequest in this case
            this.WidthRequest = 350;

            this.MenuOrientations = MenuOrientation.RightToLeft;

            // You must set BackgroundColor, 
            // and you cannot put another layout with background color cover the whole View
            // otherwise, it cannot be dragged on Android
            this.BackgroundColor = Color.LightGray;

            // This is shadow view color, you can set a transparent color
            //this.BackgroundViewColor = Color.FromHex ("#CE766C");
            this.BackgroundViewColor = Color.Transparent;
        }
    }
}
