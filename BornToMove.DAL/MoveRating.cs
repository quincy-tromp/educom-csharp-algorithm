using System;

namespace BornToMove.DAL
{
	public class MoveRating
	{
        // Properties
        public int Id { get; set; }
        public Move Move { get; set; }
        public double Rating { get; set; }
        public double Vote { get; set; }
    }
}

