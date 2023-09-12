using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> unosrtedList = CreateUnsortedList(GetNumberOfElements());
            ShiftHighestSort shiftHighestSorter = new ShiftHighestSort();
            RotateSort<int> rotateSorter = new RotateSort<int>();
            Stopwatch stopWatch = new Stopwatch();

            // Measurement 1: ShiftHighestSort
            stopWatch.Start();
            List<int> sortedList_1 = shiftHighestSorter.Sort(unosrtedList);
            stopWatch.Stop();
            TimeSpan ts_1 = stopWatch.Elapsed;

            // Measurement 2: RotateSort
            stopWatch.Restart();
            List<int> sortedList_2 = rotateSorter.Sort(unosrtedList, new Comparer());
            stopWatch.Stop();
            TimeSpan ts_2 = stopWatch.Elapsed;

            ShowList("Unsorted list", unosrtedList);
            // Result 1: ShiftHighestSort
            ShowList("Sorted list 1, ShiftHighestSort", sortedList_1);
            ValidateSortedList(sortedList_1);
            Console.WriteLine("RunTime " + ts_1);
            // Result 2: RotateSort
            ShowList("Sorted list 2, RotateSort", sortedList_2);
            ValidateSortedList(sortedList_2);
            Console.WriteLine("RunTime " + ts_2);
        }

        private static int GetNumberOfElements()
        {
            Console.WriteLine("How many elements should the list contain? ");
            int numberOfElements = Convert.ToInt32(Console.ReadLine());
            return numberOfElements;
        }
        private static List<int> CreateUnsortedList(int numberOfElements)
        {
            List<int> unsortedList = new(numberOfElements);
            Random random = new Random();
            for (int i = 0;  i < numberOfElements; i++)
            {
                unsortedList.Add(random.Next(-99, 99));
            }
            return unsortedList;
        }

        private static void ValidateSortedList(List<int> array)
        {
            bool listIsValid = true;
            for (int i = 0; i < array.Count - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    listIsValid = false;
                    Console.WriteLine("\nFailure: Sorted list FAILED the validation check.");
                    break;
                }
            }
            if (listIsValid == true)
            {
                Console.WriteLine("\nSuccess: Sorted list PASSED the validation check.");
            }
        }

        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        private static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 100)
            {
                count = 300; // Do not show more than 300 numbers
            }
            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
