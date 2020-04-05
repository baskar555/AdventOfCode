using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode
{
    public class Guards
    {
        public static Dictionary<int, int> guardTotalSleepTime = new Dictionary<int, int>();
        public static Dictionary<DateTime, DutyTimeOfADay> dailyDuty = new Dictionary<DateTime, DutyTimeOfADay>();
        public class DutyTimeOfADay
        {
            public int guardId;
            public List<int> action = new List<int>(63);

            public DutyTimeOfADay()
            {
                guardId = -1;
                action = Enumerable.Repeat(0, 63).ToList();
            }
            public DutyTimeOfADay(int gId)
            {
                guardId = gId;
                action = Enumerable.Repeat(0, 63).ToList();
            }
        }
        static void Main(string[] args)
        {
            //DutyTimeOfADay guardsDutyTimes = new DutyTimeOfADay();
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                int year = Int32.Parse(line.Substring(1, 4));
                int month = Int32.Parse(line.Substring(6, 2));
                int day = Int32.Parse(line.Substring(9, 2));
                int hour = Int32.Parse(line.Substring(12, 2));
                int min = Int32.Parse(line.Substring(15, 2));
                DateTime date = new DateTime(year, month, day);
                //Console.WriteLine("Line = {0}. DateTime = {1}. Line[19] = {2}", line, date.ToString(), line[19]);

                // Check if the string contains Guard beginning the Shift (Example- "[1518-05-17 23:59] Guard #2593 begins shift")
                if (line[19] == 'G')
                {
                    int guardIdendIdx = line.IndexOf(' ', 26);
                    int guardId = Int32.Parse(line.Substring(26, guardIdendIdx - 26));
                    // Guard begins shift at 23:xx => his time starts the next day
                    if (hour != 0)
                    {
                        DateTime newDate = date.AddDays(1);
                        date = newDate;
                    }
                    // Key not present -> Add
                    if (!dailyDuty.ContainsKey(date))
                    {
                        dailyDuty.Add(date, new DutyTimeOfADay(guardId));
                    }
                    else
                    {
                        //Key present. Update the GuardID
                        DutyTimeOfADay daysDuty = dailyDuty[date];
                        /*if (guardId != -1)
                        {
                            Console.WriteLine("GuardId already present. Something wrong. Old {0}. New {1}", guardId, daysDuty.guardId);
                        }*/
                        daysDuty.guardId = guardId;
                        dailyDuty[date] = daysDuty;
                    }
                }
                else if (line[19] == 'f')
                {
                    //[1518-02-27 00:57] falls asleep

                    // Key not present -> Add
                    if (!dailyDuty.ContainsKey(date))
                    {
                        dailyDuty.Add(date, new DutyTimeOfADay());
                    }
                    //Update the sleep start time
                    DutyTimeOfADay daysDuty = dailyDuty[date];
                    daysDuty.action[min] = -1;
                    dailyDuty[date] = daysDuty;
                }
                else if (line[19] == 'w')
                {
                    // [1518-06-18 00:08] wakes up

                    // Key not present -> Add
                    if (!dailyDuty.ContainsKey(date))
                    {
                        dailyDuty.Add(date, new DutyTimeOfADay());
                    }
                    //Update the sleep start time
                    DutyTimeOfADay daysDuty = dailyDuty[date];
                    daysDuty.action[min] = 2;
                    dailyDuty[date] = daysDuty;
                }
                //Console.WriteLine("DateTime = {0} & DictValue = {1}", date.ToString(), dailyDuty[date].guardId);
            }

            foreach (KeyValuePair<DateTime, DutyTimeOfADay> kvp in dailyDuty)
            {
                //Console.WriteLine("DateTime = {0} & GuardId = {1}", kvp.Key.ToString(), kvp.Value.guardId);

                DutyTimeOfADay dutyTimeOfADay = kvp.Value;
                int gId = dutyTimeOfADay.guardId;
                List<int> dutyTime = dutyTimeOfADay.action;
                int totalSleepTime = 0;
                // if (gId == 727)
                // {
                //     for (int i = 0; i < dutyTime.Count; i++)
                //     {
                //         Console.Write(dutyTime[i] + " ");
                //     }
                //     Console.WriteLine();
                // }
                for (int i = 0; i < dutyTime.Count; i++)
                {
                    if (dutyTime[i] == -1)
                    {
                        dutyTime[i] = 1;
                        i++;
                        totalSleepTime++;
                        while (dutyTime[i] != 2)
                        {
                            dutyTime[i] = 1;
                            totalSleepTime++;
                            i++;
                        }
                        dutyTime[i] = 0;
                        //totalSleepTime++;
                    }
                }
                if (!guardTotalSleepTime.ContainsKey(gId))
                {
                    guardTotalSleepTime.Add(gId, totalSleepTime);
                }
                else
                {
                    guardTotalSleepTime[gId] += totalSleepTime;
                }

            }
            int maxMin = -1;
            int guardNeeded = 0;
            foreach (KeyValuePair<int, int> kvp in guardTotalSleepTime)
            {
                Console.WriteLine("Guard {0} slept for {1} minutes", kvp.Key, kvp.Value);
                if (kvp.Value > maxMin)
                {
                    maxMin = kvp.Value;
                    guardNeeded = kvp.Key;
                }
            }
            Console.WriteLine("Guard {0} slept for {1} minutes", guardNeeded, maxMin);

            List<int> maxSleepAtMin = new List<int>(63);
            maxSleepAtMin = Enumerable.Repeat(0, 63).ToList();
            foreach (KeyValuePair<DateTime, DutyTimeOfADay> kvp in dailyDuty)
            {
                if (kvp.Value.guardId == guardNeeded)
                {
                    for (int i = 0; i < kvp.Value.action.Count; i++)
                    {
                        Console.Write(kvp.Value.action[i] + " ");
                        if (kvp.Value.action[i] == 1)
                        {
                            maxSleepAtMin[i]++;
                        }
                    }
                    Console.WriteLine();
                }
            }

            int idxOfMaxSleep = -1, maxSleep = 0;
            for (int i = 0; i < maxSleepAtMin.Count; i++)
            {
                if (maxSleep <= maxSleepAtMin[i])
                {
                    maxSleep = maxSleepAtMin[i];
                    idxOfMaxSleep = i;
                }
            }

            Console.WriteLine("Guard {0} slept maximum at {1} minute. The product = {2}", guardNeeded, idxOfMaxSleep, guardNeeded * idxOfMaxSleep);

            // PART B Solution starts
            // Of all guards, which guard is most frequently asleep on the same minute?
            List<int> maxSleepAtMinB = new List<int>(63);
            Dictionary<int, List<int>> guardMaxSleepMin = new Dictionary<int, List<int>>();
            foreach (KeyValuePair<DateTime, DutyTimeOfADay> kvp in dailyDuty)
            {
                int guardId = kvp.Value.guardId;
                if (!guardMaxSleepMin.ContainsKey(guardId))
                {
                    maxSleepAtMinB = Enumerable.Repeat(0, 63).ToList();
                    guardMaxSleepMin.Add(guardId, maxSleepAtMinB);
                }
                List<int> currGuardSleepAtMin = guardMaxSleepMin[guardId];
                for (int i = 0; i < kvp.Value.action.Count; i++)
                {
                    if (kvp.Value.action[i] == 1)
                    {
                        currGuardSleepAtMin[i]++;
                    }
                }
                guardMaxSleepMin[guardId] = currGuardSleepAtMin;
            }

            int maxSleepForATime = 0;
            int idxOfGuard = 0;
            int minuteOfMaxSleep = 0;
            foreach (KeyValuePair<int, List<int>> kvp in guardMaxSleepMin)
            {
                Console.WriteLine("Guard {0}  details", kvp.Key);
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    Console.Write(kvp.Value[i] + " ");
                    if (kvp.Value[i] > maxSleepForATime)
                    {
                        maxSleepForATime = kvp.Value[i];
                        idxOfGuard = kvp.Key;
                        minuteOfMaxSleep = i;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Guard {0} slept for maximum minutes at {1}. The product = {2}", idxOfGuard, minuteOfMaxSleep, idxOfGuard * minuteOfMaxSleep);
        }

    }
}