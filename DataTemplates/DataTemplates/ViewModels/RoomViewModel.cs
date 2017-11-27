using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

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

        public Command OkButton {
            get {
                return new Command (() => {
                    App.RoomsViewModel.BookRoomCommand.Execute(this);
                });
            }
        }

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

        private string meetingTitle = "";
        public string MeetingTitle 
        { 
            get
            {
                return this.meetingTitle; 
            }
            set
            {
                if (this.meetingTitle != value)
                {
                    this.meetingTitle = value;
                    if (this.meetingTitle == string.Empty) 
                    {
                        EnableOkButton = false;
                    }
                    else 
                    {
                        EnableOkButton = true;
                    }
                }
            }
        }

        public IList<TimeSlotViewModel> TimeSlots { get; set; }

        private bool enableOkButton = false;
        public bool EnableOkButton 
        {
            get
            {
                return this.enableOkButton;
            }
            set
            {
                if (this.enableOkButton != value)
                {
                    this.enableOkButton = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
