using System;
namespace AdventOfCode
{
    class CheckSum
    {
        static void Main(string[] args)
        {
            int checkSum, twosCount = 0, threesCount = 0;
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                int[] freq = new int[26];
                bool twoPresent = false, threePresent = false;
                for (int i = 0; i < line.Length; i++)
                {
                    freq[line[i] - 'a']++;
                }
                for (int i = 0; i < 26; i++)
                {
                    if (freq[i] == 2)
                    {
                        twoPresent = true;
                    }
                    else if (freq[i] == 3)
                    {
                        threePresent = true;
                    }
                }
                if (twoPresent)
                {
                    twosCount++;
                }
                if (threePresent)
                {
                    threesCount++;
                }
            }
            checkSum = twosCount * threesCount;
            Console.WriteLine("Sum is : {0}", checkSum);
        }
    }
}