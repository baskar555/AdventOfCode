using System;
using System.Collections.Generic;
namespace AdventOfCode
{
    public class Fabric
    {
        static void Main(string[] args)
        {
            //new Fabric().Initialize();
            int[,] fabric = new int[1500, 1500];
            int ans = 0, uniqueClaim = -1;
            string line;
            char[] delimiterChars = { ' ', '#', '@', ',', ':', 'x' };
            Dictionary<int, List<int>> claims = new Dictionary<int, List<int>>();
            while ((line = Console.ReadLine()) != null)
            {
                string[] split = line.Split(delimiterChars);
                List<int> dim = new List<int>();
                for (int i = 0; i < split.Length; i++)
                {
                    if (!String.IsNullOrEmpty(split[i]))
                    {
                        dim.Add(Int32.Parse(split[i]));
                    }
                }
                int x1, x2, y1, y2;
                //bool noChange = true;
                x1 = dim[2];
                y1 = dim[1];
                x2 = x1 + dim[4];
                y2 = y1 + dim[3];
                for (int i = x1; i < x2; i++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        fabric[i, j] += 1;
                    }
                }
                claims.Add(dim[0], new List<int>(dim));
            }

            for (int i = 0; i < 1500; i++)
            {
                for (int j = 0; j < 1500; j++)
                {
                    if (fabric[i, j] > 1)
                    {
                        ans++;
                    }
                }
            }
            Console.WriteLine(ans);

            for (int k = 0; k < claims.Count; k++)
            {
                List<int> dim = claims[k + 1];
                int x1, x2, y1, y2;
                bool overlap = false;
                x1 = dim[2];
                y1 = dim[1];
                x2 = x1 + dim[4];
                y2 = y1 + dim[3];
                for (int i = x1; i < x2; i++)
                {
                    for (int j = y1; j < y2; j++)
                    {
                        if (fabric[i, j] > 1)
                        {
                            overlap = true;
                            break;
                        }
                    }
                    if (overlap)
                    {
                        break;
                    }
                }
                if (!overlap)
                {
                    uniqueClaim = k + 1;
                }
            }
            Console.WriteLine(uniqueClaim);
        }

    }
}