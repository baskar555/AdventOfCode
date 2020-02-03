using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    class Frequency_B
    {
        static void Main(string[] args)
        {
            int feqSum = 0;
            string line;
            HashSet<int> hashSet = new HashSet<int>();
            List<int> list = new List<int>();
            while ((line = Console.ReadLine()) != null)
            {
                list.Add(Int32.Parse(line));
            }
            for (int i = 0; ; i++)
            {
                if (i == list.Count)
                {
                    i = 0;
                }
                int freq = list[i];
                feqSum += freq;
                if (hashSet.Contains(feqSum))
                {
                    Console.WriteLine("Found - {0}", feqSum);
                    break;
                }
                hashSet.Add(feqSum);
                Console.WriteLine("curr input: {0}. SumSoFar: {1}", freq, feqSum);
            }
            Console.WriteLine("Sum is : {0}", feqSum);
        }
    }
}