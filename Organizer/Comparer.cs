using System;
using System.Collections.Generic;

namespace Organizer
{
    public class Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return Comparer<int>.Default.Compare(x, y);
        }
    }
}

