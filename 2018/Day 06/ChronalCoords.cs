using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode
{
    public class ChronalCoords
    {
        public class Point
        {
            public int x, y;
            public Point(int a, int b)
            {
                x = a;
                y = b;
            }
        }

        public int getManhattanDist(Point a, Point b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        static void Main(string[] args)
        {
            string line;
            string[] stringseparator = new string[] { ", " };
            string[] result;

            List<Point> points = new List<Point>();
            int left, top, right, bot;
            left = top = Int32.MaxValue;
            bot = right = Int32.MinValue;
            while ((line = Console.ReadLine()) != null)
            {
                result = line.Split(stringseparator, StringSplitOptions.None);
                int x = Int32.Parse(result[1]);
                int y = Int32.Parse(result[0]);
                points.Add(new Point(x, y));
                if (x < left)
                {
                    left = x;
                }
                if (x > right)
                {
                    right = x;
                }
                if (y < top)
                {
                    top = y;
                }
                if (y > bot)
                {
                    bot = y;
                }

            }
            Console.WriteLine("left: {0}. Top:{1}. Right:{2}. Bot:{3}", left, top, right, bot);
            int[,] coords = new int[bot + 2, right + 2];
            for (int i = top; i <= bot; i++)
            {
                for (int j = left; j <= right; j++)
                {
                    int minDist = Int32.MaxValue;
                    int minDistIdx = -1;
                    // For a (i,j) point inside the 2-D space, compare with every other points and find with which point it has the min dist
                    for (int k = 0; k < points.Count; k++)
                    {
                        int dist = new ChronalCoords().getManhattanDist(points[k], new Point(j, i));
                        if (dist <= minDist)
                        {
                            minDist = dist;
                            minDistIdx = k + 1;
                        }
                    }
                    int noOfRep = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        int dist = new ChronalCoords().getManhattanDist(points[k], new Point(j, i));
                        if (dist == minDist)
                        {
                            //Console.WriteLine("-1 at i={0}, j={1}, dist = {2}", i, j, dist);
                            noOfRep++;
                        }
                        if (noOfRep > 1)
                        {
                            minDist = -1;
                            minDistIdx = -1;
                            break;
                        }
                    }
                    coords[i, j] = minDistIdx;
                    Console.Write(minDistIdx.ToString().PadLeft(5));
                }
                Console.WriteLine();
            }

            List<int> interestedPoints = new List<int>();
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];
                if (p.y == bot || p.y == top || p.x == left || p.x == right)
                {
                    continue;
                }
                Console.WriteLine("Intersted Point. x={0}, y={1}", p.x, p.y);
                interestedPoints.Add(i + 1);
            }

            int maxAns = 0;
            for (int k = 0; k < interestedPoints.Count; k++)
            {
                int countOfCurrentpoint = 0;
                for (int i = top; i <= bot; i++)
                {
                    for (int j = left; j <= right; j++)
                    {
                        if (coords[i, j] == interestedPoints[k])
                        {
                            countOfCurrentpoint++;
                        }
                    }
                }
                Console.WriteLine("Area of Current Point {0} = {1}", interestedPoints[k], countOfCurrentpoint);
                if (countOfCurrentpoint > maxAns)
                {
                    maxAns = countOfCurrentpoint;
                }
            }

            Console.WriteLine("Max area = {0}", maxAns);

            int[,] coordsArea = new int[bot + 2, right + 2];
            int areaRegionCount = 0;
            int thershold = 10000;
            for (int i = top; i <= bot; i++)
            {
                for (int j = left; j <= right; j++)
                {
                    int area = 0;
                    string arearep = ".";
                    // For a (i,j) point inside the 2-D space, compare with every other points and find if it fits within the thershold
                    for (int k = 0; k < points.Count; k++)
                    {
                        int dist = new ChronalCoords().getManhattanDist(points[k], new Point(j, i));
                        area += dist;
                        if (area > thershold)
                        {
                            break;
                        }
                    }
                    if (area < thershold)
                    {
                        coordsArea[i, j] = area;
                        areaRegionCount++;
                        arearep = "#";
                    }
                    else
                    {
                        coordsArea[i, j] = -1;
                    }
                    //Console.Write(coordsArea[i, j].ToString().PadLeft(5));
                    Console.Write(arearep);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Area Region Count = {0}", areaRegionCount);
        }
    }
}