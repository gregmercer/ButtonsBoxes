using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DataTemplates.ViewModels;

namespace DataTemplates.Pages
{
    public partial class RoomDetailView : ContentView
    {
        public RoomDetailView()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<RoomsViewModel, int>(this, "BookRoomResult", (sender, arg) => 
            {
                if (arg == (int)BookRoomResults.Success)
                {
                    App.Current.MainPage.Navigation.PopAsync();
                }
            });

            CancelButton.Clicked += (object sender, EventArgs e) => 
            {   
                App.Current.MainPage.Navigation.PopAsync();
            };
        }
    }
}
