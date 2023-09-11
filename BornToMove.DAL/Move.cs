using System;

namespace BornToMove.DAL
{
	public class Move
	{
		// Properties
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int SweatRate { get; set; }
        virtual public ICollection<MoveRating>? Ratings { get; set; }
    }
}

