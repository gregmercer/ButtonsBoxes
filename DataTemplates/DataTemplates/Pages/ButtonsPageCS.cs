using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SlideOverKit;

using DataTemplates.Views;

namespace DataTemplates.Pages
{
    public class ButtonsPageCS : MenuContainerPage
    {
        ListView listView = new ListView { };

        public ButtonsPageCS()
        {
            Title = "Buttons";
            Icon = "csharp.png";

            // Filter Toolbar Item

            ToolbarItem toolbarItem = new ToolbarItem
            { 
                Text = "F", 
                Command = ShowRoomsFilterPage,
            };

            this.ToolbarItems.Add(
                toolbarItem
            );

            this.SlideMenu = new RoomsFilterPage();

            // Rooms DataTemplate

            var roomsDataTemplate = new DataTemplate(() => {

                var grid = new Grid();

                grid.RowDefinitions.Add(new RowDefinition { Height = 20 });
                grid.RowDefinitions.Add(new RowDefinition { Height = 120 });

                grid.RowSpacing = 0;
                grid.ColumnSpacing = 10;

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
                timeSlotsLayout.Padding = 20;
                timeSlotsLayout.WidthRequest = 300;
                timeSlotsLayout.HorizontalOptions = LayoutOptions.Start;
                timeSlotsLayout.SetBinding(TimeSlotsButtonLayout.TimeSlotsSourceProperty, "TimeSlots");
                grid.Children.Add(timeSlotsLayout, 0, 3, 1, 2);

                return new ViewCell
                {
                    View = grid
                };

            });

            // Rooms ListView

            listView = new ListView 
            { 
                ItemsSource = App.RoomsViewModel.Rooms,  
                ItemTemplate = roomsDataTemplate, 
                RowHeight = 150,
                SeparatorColor = Color.Transparent,
            };
            listView.Margin = 10;

            // Next Button

            Button nextButton = new Button
            {
                Text = "Next",
                BindingContext = App.RoomsViewModel,
                Command = GoToRoomDetailPage,
                WidthRequest = 60.0,
                HeightRequest = 40.0,
                BorderWidth = 1,
                BorderColor = Color.Green,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
                FontAttributes = FontAttributes.Bold,
                FontSize = 11.0,
                BackgroundColor = Color.Green,
                TextColor = Color.White,
            };
            nextButton.SetBinding(Button.IsEnabledProperty, "EnableRoomDetailNextButton");

            nextButton.Triggers.Add(new Trigger(typeof(Button))
            {
                Property = Button.IsEnabledProperty,
                Value = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Button.BackgroundColorProperty,
                        Value = "Green"
                    }
                }
            });
            nextButton.Triggers.Add(new Trigger(typeof(Button))
            {
                Property = Button.IsEnabledProperty,
                Value = false,
                Setters =
                {
                    new Setter
                    {
                        Property = Button.BackgroundColorProperty,
                        Value = "LightGray"
                    }
                }
            });

            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                WidthRequest = 300,
                Children = {
                    new Label {
                        Text = "Buttons List",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    listView,
                    nextButton,
                }
            };
        }

        public Command ShowRoomsFilterPage {
            get {
                return new Command (() => {
                    this.ShowMenu();
                });
            }
        }

        public Command GoToRoomDetailPage {
            get {
                return new Command (() => {
                    Navigation.PushAsync(new RoomDetailPage());
                });
            }
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



