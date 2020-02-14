using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    class CheckSum_B
    {
        static void Main(string[] args)
        {
            string line;
            List<String> lines = new List<string>();
            while ((line = Console.ReadLine()) != null)
            {
                lines.Add(line);
            }
            for (int i = 0; i < lines.Count; i++)
            {
                String firstWord = lines[i];
                for (int j = i + 1; j < lines.Count; j++)
                {
                    String secondWord = lines[j];
                    int differences = 0;
                    String final = "";
                    for (int k = 0; k < secondWord.Length; k++)
                    {
                        if (firstWord[k] != secondWord[k])
                        {
                            differences++;
                            if (differences == 2)
                            {
                                break;
                            }
                        }
                        else
                        {
                            final += secondWord[k];
                        }
                    }
                    if (differences == 1)
                    {
                        Console.WriteLine(final);
                        break;
                    }
                }
            }
        }
    }
}