using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XFShapeView;

using DataTemplates.ViewModels;

namespace DataTemplates.Views
{
    public class TimeSlotsBoxLayout : Layout<View>
    {
        Dictionary<Size, LayoutData> layoutDataCache = new Dictionary<Size, LayoutData>();

        public TimeSlotsBoxLayout()
        {
        }

        public static readonly BindableProperty TimeSlotsSourceProperty = BindableProperty.Create(
            propertyName: "TimeSlotsSource",
            returnType: typeof(IList<TimeSlotViewModel>),
            declaringType: typeof(TimeSlotsBoxLayout),
            defaultValue: new List<TimeSlotViewModel>(),
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                TimeSlotsBoxLayout tsLayout = bindable as TimeSlotsBoxLayout;
                
                /*

                IList<string> timeSlotTimes = newvalue as IList<string>;
                foreach (string timeSlotTime in timeSlotTimes)
                {
                    //
                    // ShapeView Box Approach - Begin
                    //

                    var timeSlotLabel = new Label
                    {
                        Text = timeSlotTime,
                        TextColor = Color.Green,
                        FontSize = 14.0,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Fill,
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                    };

                    var box = new ShapeView
                    {
                        Content = timeSlotLabel,
                        BindingContext = tsView.IndexLabel,
                        ShapeType = ShapeType.Box,
                        WidthRequest = 60.0,
                        HeightRequest = 40.0,
                        Color = Color.White,
                        BorderColor = Color.Green,
                        BorderWidth = 1f,
                        CornerRadius = 5,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                    };

                    var boxTap = new TapGestureRecognizer();
                    boxTap.Tapped += (s, e) =>
                    {
                        ShapeView timeSlotBox = s as ShapeView;
                        var timeSlot = timeSlotBox.Content as Label;

                        if (timeSlot.BackgroundColor == Color.Green)
                        {
                            timeSlot.BackgroundColor = Color.White;
                            timeSlot.TextColor = Color.Green;

                            timeSlotBox.Color = Color.White;
                            timeSlotBox.BorderColor = Color.Green;
                            timeSlotBox.BorderWidth = 1f;
                            timeSlotBox.CornerRadius = 5;
                        }
                        else
                        {
                            timeSlot.BackgroundColor = Color.Green;
                            timeSlot.TextColor = Color.White;

                            timeSlotBox.Color = Color.Green;
                            timeSlotBox.BorderColor = Color.Green;
                            timeSlotBox.BorderWidth = 1f;
                            timeSlotBox.CornerRadius = 5;
                        }

                        Label indexLabel = timeSlot.BindingContext as Label;
                        App.RoomsViewModel.Position = Int32.Parse(indexLabel.Text);
                    };
                    box.GestureRecognizers.Add(boxTap);

                    tsView.Children.Add(box);

                    //
                    // ShapeView Box Approach - End
                    //

                }

                */

            }
        );

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


