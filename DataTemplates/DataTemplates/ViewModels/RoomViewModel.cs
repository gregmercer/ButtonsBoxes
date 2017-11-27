using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DataTemplates.ViewModels
{
    public class RoomViewModel : SimpleViewModel
    {
        public RoomViewModel ()
        {
        }

        public override string ToString()
        {
            return Name;
        }

        // Commands

        // Properties

        public int Index { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }

        public string MeetingTitle { get; set; }

        public IList<TimeSlotViewModel> TimeSlots { get; set; }
    }
}
