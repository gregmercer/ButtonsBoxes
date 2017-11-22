using System.Collections.Generic;

namespace DataTemplates.Model
{
	public class Room
	{
        public int Index { get; private set; }

		public string Name { get; private set; }

        public IList<string> TimeSlot { get; set; }

        public Room (int index, string name, IList<string> timeslot)
		{
            Index = index;
			Name = name;
            TimeSlot = timeslot;
		}

		public override string ToString ()
		{
			return Name;
		}
	}
}

