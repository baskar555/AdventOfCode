using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode
{
    public class Polymers
    {
        public string reactPolymer(string polymer)
        {
            int diff = 'a' - 'A';
            //Console.WriteLine(polymer);
            while (true)
            {
                string dup = "";
                bool ionPairFound = false;
                for (int i = 0; i < polymer.Length; i++)
                {
                    if (i + 1 < polymer.Length)
                    {
                        if (polymer[i] >= 'A' && polymer[i] <= 'Z' && (polymer[i + 1] == polymer[i] + diff))
                        {
                            i += 2;
                            ionPairFound = true;
                        }
                        else if (polymer[i] >= 'a' && polymer[i] <= 'z' && (polymer[i + 1] == polymer[i] - diff))
                        {
                            i += 2;
                            ionPairFound = true;
                        }
                    }
                    if (i < polymer.Length)
                    {
                        dup += polymer[i];
                    }
                    //Console.Write(dup + "  ");
                }
                polymer = dup;
                //Console.WriteLine(polymer);
                if (!ionPairFound)
                {
                    break;
                }
            }
            return polymer;
        }
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string poly1 = line;
                int diff = 'a' - 'A';
                string poly = new Polymers().reactPolymer(poly1);
                Console.WriteLine(poly.Length + "-" + poly);

                // PART B starts here
                int minLen = Int32.MaxValue;
                string ans = "";
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    string poly2 = "";
                    for (int j = 0; j < poly1.Length; j++)
                    {
                        if (poly1[j] == i || poly1[j] == i + diff)
                        {
                            continue;
                        }
                        poly2 += poly1[j];
                    }

                    //Console.WriteLine("Before: " + poly2.Length + "-" + poly2);
                    poly = new Polymers().reactPolymer(poly2);
                    if (poly.Length < minLen)
                    {
                        minLen = poly.Length;
                        ans = poly;
                    }
                    //Console.WriteLine("After: " + poly.Length + "-" + poly);
                }
                Console.WriteLine("Length: {0}. String = {1}", minLen, ans);
            }
        }
    }
}