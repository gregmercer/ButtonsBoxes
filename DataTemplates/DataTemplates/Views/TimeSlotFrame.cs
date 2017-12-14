using System;

using Xamarin.Forms;

namespace DataTemplates.Views
{
    public class TimeSlotFrame : Frame
    {
        public TimeSlotFrame()
        {
        }

        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
            propertyName: "Selected",
            returnType: typeof(Boolean),
            declaringType: typeof(TimeSlotFrame),
            defaultValue: false,
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                TimeSlotFrame timeSlotFrame = bindable as TimeSlotFrame;
                if (timeSlotFrame == null)
                {
                    return;
                }

                Boolean selected = (Boolean)newvalue;

                if (selected)
                {
                    timeSlotFrame.BackgroundColor = Color.Green;
                    Label timeSlotLabel = timeSlotFrame?.Content as Label;
                    timeSlotLabel.TextColor = Color.White;
                }
                else
                {
                    timeSlotFrame.BackgroundColor = Color.White;
                    Label timeSlotLabel = timeSlotFrame?.Content as Label;
                    timeSlotLabel.TextColor = Color.Green;
                }
            }
        );
    }
}
