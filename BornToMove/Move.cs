using System;

namespace BornToMove
{
	public class Move
	{
		public int Id { get; }
		public string Name { get;  }
		public string Description { get; }
		public int SweatRate { get; }

		public Move(int id, string name, string description, int sweatRate)
		{
			this.Id = id;
			this.Name = name;
			this.Description = description;
			this.SweatRate = sweatRate;
		}
	}

}

