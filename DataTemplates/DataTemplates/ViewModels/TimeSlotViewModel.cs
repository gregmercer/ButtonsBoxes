using System;
using Xamarin.Forms;

using DataTemplates.Views;

namespace DataTemplates.ViewModels
{
    public class TimeSlotViewModel : SimpleViewModel
    {
        public TimeSlotViewModel()
        {
        }

        public Boolean Available { get; set; }

        public RoomViewModel RoomViewModel { get; set; }

        public TimeSlotFrame TimeSlotFrame { get; set; }
        public TimeSlotButton TimeSlotButton { get; set; }
        public TimeSlotBox TimeSlotBox { get; set; }

        Boolean selected = false;
        public Boolean Selected { 
            get { return this.selected; }  
            set
            {
                if (this.selected == value) {
                    return;
                }
                this.selected = value;
                RaisePropertyChanged();
            }
        }

        string startTime = "";
        public string StartTime {
            get { return this.startTime; }
            set
            {
                if (this.startTime == value) {
                    return;
                }

                this.startTime = value;
            }
        }
    }
}
