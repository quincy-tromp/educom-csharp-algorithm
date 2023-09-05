using System;

namespace BornToMove
{
	public class Move
	{
		public int id { get; }
		public string name { get;  }
		public string description { get; }
		public int sweatRate { get; }

		public Move(int id, string name, string description, int sweatRate)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.sweatRate = sweatRate;
		}
	}

}

