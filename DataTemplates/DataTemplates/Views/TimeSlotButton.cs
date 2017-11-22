using System;

using Xamarin.Forms;

namespace DataTemplates.Views
{
    public class TimeSlotButton : Button
    {
        public TimeSlotButton()
        {
        }

        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
            propertyName: "Selected",
            returnType: typeof(Boolean),
            declaringType: typeof(TimeSlotButton),
            defaultValue: false,
            propertyChanged: (bindable, oldvalue, newvalue) =>
            {
                TimeSlotButton timeSlotButton = bindable as TimeSlotButton;

                Boolean selected = (Boolean)newvalue;

                if (selected)
                {
                    timeSlotButton.BackgroundColor = Color.Green;
                    timeSlotButton.TextColor = Color.White;
                }
                else
                {
                    timeSlotButton.BackgroundColor = Color.White;
                    timeSlotButton.TextColor = Color.Green;
                }
            }
        );

    }
}
