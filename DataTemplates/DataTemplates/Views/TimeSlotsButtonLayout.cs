﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XFShapeView;

using DataTemplates.ViewModels;
using DataTemplates.Views;

namespace DataTemplates.Views
{
    public class TimeSlotsButtonLayout : Layout<View>
    {
        Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();

        public TimeSlotsButtonLayout()
        {
        }

        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
            propertyName: "ColumnSpacing",
            returnType: typeof(double),
            declaringType: typeof(TimeSlotsButtonLayout),
            defaultValue: 5.0,
            propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            ((TimeSlotsButtonLayout)bindable).InvalidateLayout();
        }
        );

        public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(
            propertyName: "RowSpacing",
            returnType: typeof(double),
            declaringType: typeof(TimeSlotsButtonLayout),
            defaultValue: 5.0,
            propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            ((TimeSlotsButtonLayout)bindable).InvalidateLayout();
        }
        );

        public static readonly BindableProperty TimeSlotsSourceProperty = BindableProperty.Create(
            propertyName: "TimeSlotsSource",
            returnType: typeof(IList<TimeSlotViewModel>),
            declaringType: typeof(TimeSlotsButtonLayout),
            defaultValue: new List<TimeSlotViewModel>(),
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                TimeSlotsButtonLayout tsLayout = bindable as TimeSlotsButtonLayout;
                 
                IList<TimeSlotViewModel> timeSlotViewModels = newvalue as IList<TimeSlotViewModel>;
                foreach (TimeSlotViewModel timeSlotViewModel in timeSlotViewModels)
                {
                    //
                    // Button Approach - Begin
                    //

                    TimeSlotButton timeSlotButton = new TimeSlotButton
                    {
                        Text = timeSlotViewModel.StartTime,
                        BindingContext = timeSlotViewModel,
                        WidthRequest = 60.0,
                        HeightRequest = 40.0,
                        FontSize = 11.0,
                        BorderWidth = 1,
                        BorderColor = Color.Green,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Fill,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Color.White,
                        TextColor = Color.Green,
                    };
                    timeSlotButton.SetBinding(TimeSlotButton.SelectedProperty, "Selected");
                    timeSlotViewModel.TimeSlotButton = timeSlotButton;

                    timeSlotButton.Clicked += (s, e) =>
                    {
                        var timeSlot = s as Button;

                        TimeSlotViewModel timeslotViewModel = timeSlot.BindingContext as TimeSlotViewModel;

                        App.RoomsViewModel.ToggleTimeSlotCommand.Execute(timeslotViewModel);
                    };

                    tsLayout.Children.Add(timeSlotButton);

                    //
                    // Button Approach - End
                    //

                }
            }
        );

        public double ColumnSpacing
        {
            set { SetValue(ColumnSpacingProperty, value); }
            get { return (double)GetValue(ColumnSpacingProperty); }
        }

        public double RowSpacing
        {
            set { SetValue(RowSpacingProperty, value); }
            get { return (double)GetValue(RowSpacingProperty); }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            LayoutData layoutData = GetLayoutData(widthConstraint, heightConstraint);
            if (layoutData.VisibleChildCount == 0)
            {
                return new SizeRequest();
            }

            Size totalSize = new Size(layoutData.CellSize.Width * layoutData.Columns + ColumnSpacing * (layoutData.Columns - 1),
                                      layoutData.CellSize.Height * layoutData.Rows + RowSpacing * (layoutData.Rows - 1));

            return new SizeRequest(totalSize);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            LayoutData layoutData = GetLayoutData(width, height);

            if (layoutData.VisibleChildCount == 0)
            {
                return;
            }

            double xChild = x;
            double yChild = y;
            int row = 0;
            int column = 0;

            foreach (View child in Children)
            {
                if (!child.IsVisible)
                {
                    continue;
                }

                LayoutChildIntoBoundingRegion(child, new Rectangle(new Point(xChild, yChild), layoutData.CellSize));

                if (++column == layoutData.Columns)
                {
                    column = 0;
                    row++;
                    xChild = x;
                    yChild += 5.0 + layoutData.CellSize.Height;
                }
                else
                {
                    xChild += 5.0 + layoutData.CellSize.Width;
                }
            }
        }

        LayoutData GetLayoutData(double width, double height)
        {
            Size size = new Size(width, height);

            // Check if cached information is available.
            if (layoutDataCache.ContainsKey(size))
            {
                return layoutDataCache[size];
            }

            int visibleChildCount = 0;
            Size maxChildSize = new Size();
            int rows = 0;
            int columns = 0;
            LayoutData layoutData = new LayoutData();

            // Enumerate through all the children.
            foreach (View child in Children)
            {
                // Skip invisible children.
                if (!child.IsVisible)
                    continue;

                // Count the visible children.
                visibleChildCount++;

                // Get the child's requested size.
                SizeRequest childSizeRequest = child.Measure(Double.PositiveInfinity, Double.PositiveInfinity);

                // Accumulate the maximum child size.
                maxChildSize.Width = Math.Max(maxChildSize.Width, childSizeRequest.Request.Width);
                maxChildSize.Height = Math.Max(maxChildSize.Height, childSizeRequest.Request.Height);
            }

            if (visibleChildCount != 0)
            {
                // Calculate the number of rows and columns.
                if (Double.IsPositiveInfinity(width))
                {
                    columns = visibleChildCount;
                    rows = 1;
                }
                else
                {
                    columns = (int)((width + 5.0) / (maxChildSize.Width + 5.0));
                    columns = Math.Max(1, columns);
                    rows = (visibleChildCount + columns - 1) / columns;
                }

                // Now maximize the cell size based on the layout size.
                Size cellSize = new Size();

                if (Double.IsPositiveInfinity(width))
                {
                    cellSize.Width = maxChildSize.Width;
                }
                else
                {
                    cellSize.Width = (width - 5.0 * (columns - 1)) / columns;
                }

                if (Double.IsPositiveInfinity(height))
                {
                    cellSize.Height = maxChildSize.Height;
                }
                else
                {
                    cellSize.Height = (height - 5.0 * (rows - 1)) / rows;
                }

                layoutData = new LayoutData(visibleChildCount, cellSize, rows, columns);
            }

            layoutDataCache.Add(size, layoutData);

            return layoutData;
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();

            // Discard all layout information for children added or removed.
            layoutDataCache.Clear();
        }

        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();

            // Discard all layout information for child size changed.
            layoutDataCache.Clear();
        }
    }
}


