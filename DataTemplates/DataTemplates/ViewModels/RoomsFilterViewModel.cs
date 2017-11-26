using System;

using Xamarin.Forms;

namespace DataTemplates.ViewModels
{
    public class RoomsFilterViewModel
    {
        public RoomsFilterViewModel()
        {
        }

        public TimeSpan StartTime
        {
            get
            {
                var dateTime = DateTime.Now;
                return dateTime.TimeOfDay;
            }
            set
            {
            }
        }

        /*
        public Command DoOK {
            get {
                return new Command (() => {
                    var temp = 1;
                    temp++;
                });
            }
        }

        public Command DoCancel {
            get {
                return new Command (() => {
                    var temp = 1;
                    temp++;
                });
            }
        }
        */
    }
}
