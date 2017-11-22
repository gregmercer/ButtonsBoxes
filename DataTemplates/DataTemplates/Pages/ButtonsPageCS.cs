using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DataTemplates.Views;

namespace DataTemplates.Pages
{
    public class ButtonsPageCS : ContentPage
    {
        ListView listView = new ListView { };

        public ButtonsPageCS()
        {
            Title = "Buttons";
            Icon = "csharp.png";

            var roomsDataTemplate = new DataTemplate(() => {

                var grid = new Grid();

                grid.RowDefinitions.Add(new RowDefinition { Height = 20 });
                grid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });

                var indexLabel = new Label { IsVisible = false };
                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };

                indexLabel.SetBinding(Label.TextProperty, "Index");
                nameLabel.SetBinding(Label.TextProperty, "Name");

                grid.Children.Add(indexLabel, 0, 1, 0, 1);
                grid.Children.Add(nameLabel, 0, 1, 0, 1);                  

                var timeSlotsLayout = new TimeSlotsButtonLayout();
                timeSlotsLayout.SetBinding(TimeSlotsButtonLayout.TimeSlotsSourceProperty, "TimeSlots");
                grid.Children.Add(timeSlotsLayout, 0, 3, 1, 2);

                return new ViewCell
                {
                    View = grid
                };

            });

            listView = new ListView 
            { 
                ItemsSource = App.RoomsViewModel.Rooms,  
                ItemTemplate = roomsDataTemplate, 
                RowHeight = 150 
            };

            Button nextButton = new Button
            {
                Text = "Next",
                WidthRequest = 60.0,
                HeightRequest = 40.0,
                FontSize = 11.0,
                BorderWidth = 1,
                BorderColor = Color.Green,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.Green,
                TextColor = Color.White,
            };

            nextButton.Clicked += (sender, e) => 
            {
                listView.Navigation.PushAsync(new RoomDetailPage());
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children = {
                    new Label {
                        Text = "Buttons List",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    listView,
                    nextButton
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.RoomsViewModel.GetRoomsCommand.Execute(null);

            this.listView.ItemsSource = App.RoomsViewModel.Rooms;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}



