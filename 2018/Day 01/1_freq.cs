using System;
namespace AdventOfCode
{
    class Frequency
    {
        static void Main(string[] args)
        {
            int feqSum = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                int freq = Int32.Parse(line);
                feqSum += freq;
                Console.WriteLine("curr input: {0}. SumSoFar: {1}", freq, feqSum);
            }
            Console.WriteLine("Sum is : {0}", feqSum);
        }
    }
}