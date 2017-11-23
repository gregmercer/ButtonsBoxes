using System;

using Xamarin.Forms;
using XFShapeView;

namespace DataTemplates.Views
{
    public class TimeSlotBox : ShapeView
    {
        public TimeSlotBox()
        {
        }

        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
            propertyName: "Selected",
            returnType: typeof(Boolean),
            declaringType: typeof(TimeSlotBox),
            defaultValue: false,
            propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            TimeSlotBox timeSlotBox = bindable as TimeSlotBox;
            if (timeSlotBox == null)
            {
                return;
            }

            var timeSlot = timeSlotBox.Content as Label;

            Boolean selected = (Boolean)newvalue;

            if (selected)
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
        }
        );

    }
}
