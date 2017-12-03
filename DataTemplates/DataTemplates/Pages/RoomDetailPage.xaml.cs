using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DataTemplates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomDetailPage : ContentPage
    {
        public RoomDetailPage()
        {
            InitializeComponent();
            BindingContext = App.RoomsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send<RoomDetailPage>(this, "MeetingTitleSetFocus");
        }
    }
}
