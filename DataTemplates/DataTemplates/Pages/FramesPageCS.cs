using System;
using System.Collections.Generic;

using Platform = Xamarin.Forms.PlatformConfiguration;

using Xamarin.Forms;
using SlideOverKit;

using DataTemplates.Views;

namespace DataTemplates.Pages
{
    public class FramesPageCS : MenuContainerPage
    {
        ListView listView = new ListView(ListViewCachingStrategy.RecycleElement) { };

        public FramesPageCS()
        {
            Title = "Frames";
            Icon = "csharp.png";

            // SearchBar

            SearchBar searchBar = new SearchBar
            {
                Placeholder = "Enter your search here.",
            };
            searchBar.TextChanged += (sender, e) => 
            { 
                return; 
            };
            searchBar.SearchButtonPressed += (sender, e) => 
            { 
                return; 
            };

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
                CompressedLayout.SetIsHeadless(grid, true);

                grid.RowDefinitions.Add(new RowDefinition { Height = 20 });
                //grid.RowDefinitions.Add(new RowDefinition { Height = 700 });

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

                var timeSlotsLayout = new TimeSlotsFrameLayout();
                timeSlotsLayout.Padding = 20;
                timeSlotsLayout.WidthRequest = 300;
                timeSlotsLayout.HorizontalOptions = LayoutOptions.Start;
                timeSlotsLayout.SetBinding(TimeSlotsFrameLayout.TimeSlotsSourceProperty, "TimeSlots");
                grid.Children.Add(timeSlotsLayout, 0, 3, 1, 2);

                return new ViewCell
                {
                    View = grid
                };

            });

            // Rooms ListView

            listView = new ListView() 
            { 
                ItemsSource = App.RoomsViewModel.Rooms,  
                ItemTemplate = roomsDataTemplate, 
                //RowHeight = 150,
                HasUnevenRows = true,
                SeparatorColor = Color.Transparent,
            };
            listView.Margin = new Thickness(10);
            //listView.On<Platform::Android>().SetIsFastScrollEnabled(true);

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

            var testLabel = new Label
            {
                Text = "Yo, yo, Dude",
                TextColor = Color.Green,
                //BackgroundColor = Color.Transparent,
                FontSize = 14.0,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };

            var testFrame = new TimeSlotFrame {
                Content = testLabel,
                WidthRequest = 60.0,
                HeightRequest = 40.0,
                //FontSize = 11.0,
                //BorderWidth = 1,
                //BorderColor = Color.Green,
                OutlineColor = Color.Green,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
                //FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.White,
                //TextColor = Color.Green,
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),
                WidthRequest = 300,
                Children = {
                    /*
                    new Label {
                        Text = "Buttons List",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    }, 
                    */
                    searchBar,
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



