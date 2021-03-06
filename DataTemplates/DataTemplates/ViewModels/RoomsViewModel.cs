﻿using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

using DataTemplates.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataTemplates.ViewModels
{
    public enum BookRoomResults
    {
        Failed,
        Success,
    }

    public class RoomsViewModel : SimpleViewModel
    {
        public ICommand GetRoomsCommand { get; private set; }
        public ICommand ToggleTimeSlotCommand { get; private set; }
        public ICommand StartRoomBookingCommand { get; private set; }
        public ICommand BookRoomCommand { get; private set; }

        public RoomsViewModel()
        {
            GetRoomsCommand = new Command(async () => await GetRooms());
            ToggleTimeSlotCommand = new Command((param) => ToggleTimeSlot(param));
            BookRoomCommand = new Command((param) => BookRoom(param));
        }

        // Commands

        protected async Task GetRooms()
        {
            EnableRoomDetailNextButton = false; 

            ObservableCollection<RoomViewModel> roomsList = new ObservableCollection<RoomViewModel>{};

            for (int index = 0; index < 100; index++)
            {
                List<TimeSlotViewModel> tsvmList = new List<TimeSlotViewModel>();
                RoomViewModel rvm = new RoomViewModel() { Index = index, Name = "Room " + index, TimeSlots = tsvmList };

                var timeSlots = new List<string> { "7:00a", "7:30a", "8:00a", "8:30a", "9:00a", "9:30a" };

                foreach (string timeslot in timeSlots)
                {
                    TimeSlotViewModel tsm = new TimeSlotViewModel() { StartTime = timeslot, Available = true, RoomViewModel = rvm };

                    tsvmList.Add(tsm);
                }

                roomsList.Add(rvm);
            }

            Rooms = roomsList;
        }

        public void ToggleTimeSlot(object tsm)
        {
            TimeSlotViewModel timeSlotViewModel = tsm as TimeSlotViewModel;
            RoomViewModel roomViewModel = timeSlotViewModel.RoomViewModel;
            SelectedRoom = roomViewModel;
            var temp = timeSlotViewModel.Selected;
            timeSlotViewModel.Selected = !temp;
            if (timeSlotViewModel.Selected) 
            {
                EnableRoomDetailNextButton = true;
            }
            else
            {
                EnableRoomDetailNextButton = false;    
            }
        }

        protected async Task BookRoom(object rvm)
        {
            RoomViewModel roomViewModel = rvm as RoomViewModel;
            MessagingCenter.Send<RoomsViewModel, int>(this, "BookRoomResult", (int)BookRoomResults.Success);
            var temp = 1;
            temp++;
        }

        // Properties

        private bool enableRoomDetailNextButton = false;
        public bool EnableRoomDetailNextButton
        {
            get
            {
                return this.enableRoomDetailNextButton;
            }
            set
            {
                if (this.enableRoomDetailNextButton != value)
                {
                    this.enableRoomDetailNextButton = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RoomViewModel SelectedRoom { set; get; }

        public int Position
        {
            get
            {
                if (SelectedRoom == null)
                    return 0;

                int index = 0;
                foreach (RoomViewModel room in Rooms)
                {
                    if (room.Index == SelectedRoom.Index)
                    {
                        break;
                    }

                    index++;
                }

                return index;
            }
            set
            {
                if ((value > Rooms.Count - 1) || (value < 0))
                {
                    return;
                }
                else
                {
                    SelectedRoom = Rooms[value];
                }
            }
        }

        private ObservableCollection<RoomViewModel> rooms;
        public ObservableCollection<RoomViewModel> Rooms
        {
            get
            {
                return this.rooms;
            }
            set
            {
                rooms = value;
                RaisePropertyChanged();
            }
        }
    }
}
