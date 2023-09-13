using System;
using System.Collections.Generic;

namespace BornToMove.DAL
{
    public class RatingsComparer : IComparer<MoveRating>
    {
        public int Compare(MoveRating? x, MoveRating? y)
        {
            return Comparer<double?>.Default.Compare(x?.Rating, y?.Rating);
        }
    }

    public class RatingsConverter : IComparer<MoveAverageRating>
    {
        public int Compare(MoveAverageRating? x, MoveAverageRating? y)
        {
            return Comparer<double?>.Default.Compare(x?.AverageRating, y?.AverageRating);
        }
    }
}

