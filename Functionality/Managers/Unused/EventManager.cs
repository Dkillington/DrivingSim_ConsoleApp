using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivinSim.Functionality.Managers
{
    internal class EventManager
    {
        //public static void GetPulledOver()
        //{

        //    if (wantedLevel > 1)
        //    {
        //        isArrested = true;
        //    }

        //    if (isArrested != true)
        //    {
        //        Console.Clear();
        //        Console.SetCursorPosition(0, 0);
        //        Random rand = new Random();
        //        Stopwatch wait = new Stopwatch();
        //        wait.Start();
        //        Console.WriteLine("An officer is approaching your window...");
        //        while(wait.ElapsedMilliseconds <= 2500)
        //        { }

        //        if (speedCaughtAt > 0)
        //        {
        //            Console.Clear();
        //            List<string> speedingMessages = new List<string>();
        //            //speedingMessages.Add("\"Do you know how fast you were goin?\"");
        //            speedingMessages.Add("\"Boi... do you know I caught you at " + speedCaughtAt + " mph?\"");
        //            string speedingMessage = speedingMessages[rand.Next(0, speedingMessages.Count)];

        //            Options(speedingMessage, "Yes sir. " + speedCaughtAt + " miles per hour.", "Uh no, I wasn't really paying attention.", "Fuck off pig");
        //            if (answer == "1")
        //            {
        //                int num = rand.Next(1, 10);
        //                if (num >= 1 && num <= 5)
        //                {
        //                    Console.WriteLine("Eh you're a good kid. Ill let you go!");
        //                    Console.ReadLine();
        //                }
        //                else
        //                {
        //                    CollectFine("Speeding");
        //                }

        //                wantedLevel = 0;
        //            }
        //            else if (answer == "2")
        //            {

        //            }
        //            else if (answer == "3")
        //            {
        //                isArrested = true;
        //            }

        //            speedCaughtAt = 0;
        //        }
        //    }
        //}

        //public static void GetArrested()
        //{
        //    Random rand = new Random();
        //    Console.Clear();
        //    List<string> arrestedWays = new List<string>();

        //    if (wantedLevel == 1)
        //    {
        //        arrestedWays.Add("You were taken out of the car and cuffed!");
        //        arrestedWays.Add("You were thrown on the ground and clubbed!");
        //    }
        //    else if (wantedLevel == 2)
        //    {
        //        arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
        //        arrestedWays.Add("You were beaten!");
        //    }
        //    else if (wantedLevel == 3)
        //    {
        //        arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
        //        arrestedWays.Add("You were beaten!");
        //    }
        //    else if (wantedLevel == 4)
        //    {
        //        arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
        //        arrestedWays.Add("You were beaten!");
        //    }
        //    else if (wantedLevel == 5)
        //    {
        //        arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
        //        arrestedWays.Add("You were beaten!");
        //    }
        //    else
        //    {
        //        arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
        //        arrestedWays.Add("You were beaten!");
        //    }

        //    Console.WriteLine(arrestedWays[rand.Next(0, arrestedWays.Count)]);
        //    Console.ReadLine();
        //    wantedLevel = 0;
        //    speedCaughtAt = 0;
        //    isDriving = false;
        //}

        //public static void CollectFine(string what)
        //{
        //    what = what.ToLower();
        //    if (what == "speeding")
        //    {
        //        Console.WriteLine("You were issued a fine for speeding!");
        //    }
        //    else if (what == "resisting arrest")
        //    {
        //        Console.WriteLine("You were issued a fine for resisting arrest!");
        //    }
        //    else
        //    {

        //    }

        //    Console.ReadKey();
        //}
    }
}
