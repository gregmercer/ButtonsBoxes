using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

using DataTemplates.Model;
using System.Threading.Tasks;

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

        public IList<TimeSlotViewModel> TimeSlots { get; set; }
    }
}
