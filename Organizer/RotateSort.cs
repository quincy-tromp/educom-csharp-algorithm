﻿using System;
using System.Collections.Generic;

namespace Organizer
{
	public class RotateSort<T>
	{
        private List<T> array = new List<T>();
        private IComparer<T> Comparer;

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<T> Sort(List<T> input, IComparer<T> comparer)
        {
            array = new List<T>(input);

            Comparer = comparer;

            SortFunction(0, array.Count - 1);
            
            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low">The index within this.array to start with</param>
        /// <param name="high">The index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            if (low < high)
            {
                int pivot_location = Partitioning(low, high);
                SortFunction(low, pivot_location);
                SortFunction(pivot_location + 1, high);
            }
        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a choosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int low, int high)
        {
            T pivot = array[low];
            int leftwall = low;

            for (int i = low + 1; i <= high; i++)
            {
                if (Comparer.Compare(array[i], pivot) < 0)
                {
                    (array[i], array[leftwall]) = (array[leftwall], array[i]);
                    leftwall++;
                }
            }
            (pivot, array[leftwall]) = (array[leftwall], pivot);

            return leftwall;

        }
    }
}
