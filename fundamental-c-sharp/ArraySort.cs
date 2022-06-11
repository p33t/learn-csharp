using System;
using System.Diagnostics;

namespace fundamental_c_sharp
{
    public static class ArraySort
    {
        public static void Main(string[] args)
        {
            var arr = new[] {1, 3, 5, 7, 9};

            var ix5 = Array.BinarySearch(arr, 5);
            Trace.Assert(ix5 == 2);
            var ix7 = Array.BinarySearch(arr, 7);
            var ix6 = Array.BinarySearch(arr, 1, 3, 6);
            Trace.Assert(ix6 == ix7 * -1 -1); // bitwise complement of would-be insertion position
            
            Console.WriteLine("Success");
        }
    }
}