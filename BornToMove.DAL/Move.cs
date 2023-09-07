using System;

namespace BornToMove.DAL
{
	public class Move
	{
		public int Id { get; set; }
		public required string Name { get; init; }
		public required string Description { get; init; }
		public required int SweatRate { get; init; }
	}

}

