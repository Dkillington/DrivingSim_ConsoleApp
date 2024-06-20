using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DrivinSim
{
    public class Vehicle
    {
        public string name;
        public static int serial;
        public double value;
        public int topSpeed;
        public int acceleration;
        public int brakeSpeed;
        public int amount;
        
        public Vehicle(string _name, double _value, int _topSpeed, int _acceleration, int _brakeSpeed, int _amount)
        {
            name = _name;
            value = _value;
            acceleration = _acceleration;
            topSpeed = _topSpeed;
            brakeSpeed = _brakeSpeed;
            amount = _amount;
        }
    }
    public class DrivingSimulator
    {
        public static bool newGame = true;
        public static string answer = "";
        public static double drivingMoney = 0;
        public static double playerMoney = 0;

        public static string carName = "";
        public static int carBrakeSpeed = 150;
        public static int carAccelerateSpeed = 100;
        public static int idleSpeed = 200;
        public static int carTopSpeed = 200;

        public static double currentSpeed = 0;
        public static double nearestCopMaxSpeed = 0;
        public static double distanceLeft = 0;
        public static int theSpeedLimit = 0;
        public static int wantedLevel = 0;
        public static string roadOn = "";
        public static string upcomingRoad = "";
        public static string upcomingRoadType = "";
        public static string convoText = "";

        //Timer times
        public static int convoBaseTime = 5000;
        public static int checkDistanceBaseTime = 100;
        public static int policeSeeYouBaseTime = 100;
        public static int resistArrestBaseTime = 2000;
        public static int arrestCountdownBaseTime = 2000;

        //Laws to break
        public static bool breakingLaw = false;
        public static bool isArrested = false;
        public static bool speeding = false;

        public static bool convoActive = false;
        public static bool isDriving = false;
        public static double speedCaughtAt = 0;
        public static double checkSpeed = 0;
        public static double speedLimitCaughtAt = 0;


        public static System.Timers.Timer getDistanceTimer = new System.Timers.Timer();
        public static System.Timers.Timer policeSeeYouTimer = new System.Timers.Timer();
        public static System.Timers.Timer resistArrestTimer = new System.Timers.Timer();
        public static System.Timers.Timer convoTimer = new System.Timers.Timer();
        public static System.Timers.Timer arrestCountdown = new System.Timers.Timer();
        public static Stopwatch flashLights = new Stopwatch();
        public static Stopwatch policeWillAppear = new Stopwatch();

        public static List<int> serials = new List<int>();
        public static Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
        public static Dictionary<int, Vehicle> ownedVehicles = new Dictionary<int, Vehicle>();


        [STAThread]
        static void Main(string[] args)
        {

            //Vehicles
            int seriall = GrabSerial();
            vehicles.Add(seriall, new Vehicle("1992 Toyota Camry", 1000, 55, 100, 100, 1));
            ownedVehicles.Add(seriall, new Vehicle("1992 Toyota Camry", 1000, 55, 100, 100, 1));

            seriall = GrabSerial();
            //Timers
            {
                getDistanceTimer.Elapsed += new System.Timers.ElapsedEventHandler(CorrectDistance);
                getDistanceTimer.Interval = checkDistanceBaseTime;
                getDistanceTimer.Enabled = true;


                policeSeeYouTimer.Elapsed += new System.Timers.ElapsedEventHandler(PoliceSeeYou);
                policeSeeYouTimer.Interval = policeSeeYouBaseTime;
                policeSeeYouTimer.Enabled = true;

                policeWillAppear.Start();

                resistArrestTimer.Elapsed += new System.Timers.ElapsedEventHandler(ResistArrest);
                resistArrestTimer.Interval = resistArrestBaseTime;
                resistArrestTimer.Enabled = false;

                convoTimer.Elapsed += new System.Timers.ElapsedEventHandler(SayConvo);
                convoTimer.Interval = convoBaseTime;
                convoTimer.Enabled = false;

                arrestCountdown.Elapsed += new System.Timers.ElapsedEventHandler(ConfrontPolice);
                arrestCountdown.Interval = arrestCountdownBaseTime;
                arrestCountdown.Enabled = false;

            }


            Console.CursorVisible = false;
            while (1 == 1)
            {
                MainMenu();
            }
        }

        //Main Menus
        public static void MainMenu()
        {
            if (newGame == true)
            {
                GetNewRoad();
                newGame = false;
            }


            InitalizeCar();
            EscapeCops();
            Options("Active Car: " + carName + "\n\nMoney: $" + playerMoney.ToString("#,##0"), "Drive! [" + roadOn + "]", "Garage", "Autoshop", "Stats");
            if(answer == "1")
            {

                Drive();
            }
            else if(answer == "2")
            {
                Garage();
            }
            else if (answer == "3")
            {
                AutoShop();
            }
            else if(answer == "4")
            {
                StatsPage();
            }
        }
        public static void Drive()
        {
            arrestCountdown.Enabled = false;
            bool drive = true;
            while(drive == true)
            {
                Console.Clear();
                wantedLevel = 0;
                isArrested = false;
                policeSeeYouTimer.Enabled = true;
                resistArrestTimer.Interval = resistArrestBaseTime;
                policeWillAppear.Restart();
                isDriving = true;
                while (isDriving == true)
                {
                    GetText();
                    if (Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
                    {
                        SpeedUp();
                    }
                    else if (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down))
                    {
                        SlowDown("Brake");
                    }
                    else if (Keyboard.IsKeyDown(Key.E) && currentSpeed == 0)
                    {
                        isDriving = false;
                        drive = false;
                    }
                    else
                    {
                        SlowDown("Idle");
                    }
                }

                arrestCountdown.Enabled = false;
                if (wantedLevel >= 1)
                {
                    GetPulledOver();
                }
                
                if(isArrested == true)
                {
                    GetArrested();
                    drive = false;
                }
                Console.Clear();
                playerMoney += drivingMoney;
                drivingMoney = 0;
            }
        }
        public static void Garage()
        {

        }
        public static void AutoShop()
        {
            foreach(KeyValuePair<int, Vehicle> vehicle in vehicles)
            {

            }
        }
        public static void StatsPage()
        {

        }
        public static void GetText()
        {
            string speedingText = "";
            string resistingArrestText = "";

            Console.SetCursorPosition(0, 0);
            Console.Write("\r{0}                            ", "Active Car: " + carName + "                                         \n\n");
            Console.Write("\r{0}                            ", "Current Road: " + roadOn + "                                         \n");
            Console.Write("\r{0}                            ", "Next Road: " + upcomingRoad + ": " + Math.Round(distanceLeft,0) + " ft" + "                              \n\n");
            Console.Write("\r{0}                            ", "$: " + drivingMoney.ToString("#,##0") + "                              \n\n");
            Console.Write("\r{0}                            ", "SPEED LIMIT: " + theSpeedLimit + "\n------------------------                              \n\n");

            CheckData();

            Console.Write("\r{0}                                                                          ", convoText + "\n");
            if(resistingArrestText != "" && resistingArrestText != "Police are near...")
            {
                long num = flashLights.ElapsedMilliseconds;
                if(num < 1000)
                {
                    num = 2;
                }
                else
                {
                    while (num >= 1000)
                    {
                        num /= 1000;
                    }
                }

                if (num%2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }


            Console.Write("\r{0}                                                                          ", resistingArrestText);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("\r{0}                                                                          ", "\n" + speedingText + "                                  \n");
            Console.Write("\r{0}                                                                          ", "Speed: " + currentSpeed + " Mph                           ");



            //Console.Write("\r{0}                            ", "Nearest cop speed: " + nearestCopMaxSpeed + " Mph");

            void CheckData()
            {
                //Checks if speeding to give message.
                if (speeding == true)
                {
                    speedingText = "Speeding!";
                }
                else
                {
                    speedingText = "";
                }

                if(wantedLevel > 0)
                {
                    flashLights.Start();
                }
                else
                {
                    flashLights.Reset();
                }


                if (wantedLevel == 0)
                {
                    convoTimer.Enabled = false;
                    resistingArrestText = "";
                    if(policeWillAppear.ElapsedMilliseconds >= (policeSeeYouTimer.Interval - 3000))
                    {
                        resistingArrestText = "Police are near...";
                    }
                    else
                    {
                        resistingArrestText = "";
                    }
                }
                else if (wantedLevel == 1)
                {
                    policeWillAppear.Stop();
                    convoTimer.Enabled = false;
                    resistingArrestText = "*Being pulled over!*";
                }
                else
                {
                    policeWillAppear.Stop();
                    convoTimer.Enabled = true;
                    resistingArrestText = "RESISTING ARREST!!!   LEVEL:" + wantedLevel;
                }

                
            }
        }
        public static void GetNewRoad()
        {
            Random rand = new Random();
            if (roadOn == "")
            {
                GetUpcomingData();
            }

            roadOn = upcomingRoad;
            if (upcomingRoadType == "Highway")
            {
                int num = rand.Next(1, 10);
                if (num >= 1 && num <= 5)
                {
                    theSpeedLimit = 55;
                }
                else if (num > 5 && num <= 7)
                {
                    theSpeedLimit = 75;
                }
                else
                {
                    theSpeedLimit = 85;
                }

                distanceLeft = rand.Next(1000, 4000);
            }
            else if (upcomingRoadType == "City")
            {
                int num = rand.Next(1, 10);
                if (num >= 1 && num <= 5)
                {
                    theSpeedLimit = 35;
                }
                else if (num > 5 && num <= 7)
                {
                    theSpeedLimit = 45;
                }
                else
                {
                    theSpeedLimit = 50;
                }
                distanceLeft = rand.Next(1000, 2000);
            }
            else if (upcomingRoadType == "Road")
            {
                int num = rand.Next(1, 10);
                if (num >= 1 && num <= 5)
                {
                    theSpeedLimit = 25;
                }
                else if (num > 5 && num <= 7)
                {
                    theSpeedLimit = 30;
                }
                else
                {
                    theSpeedLimit = 35;
                }
                distanceLeft = rand.Next(10000, 30000);
            }

            GetUpcomingData();

            void GetUpcomingData()
            {
                int num = rand.Next(1, 10);
                if (num >= 1 && num <= 5)
                {
                    upcomingRoadType = "Highway";
                    List<string> highwayNames = new List<string>();
                    highwayNames.Add("Desert Highway");
                    highwayNames.Add("Arctic Highway");
                    highwayNames.Add("Highway " + rand.Next(2, 200));
                    upcomingRoad = highwayNames[rand.Next(0, highwayNames.Count)];
                }
                else if (num > 5 && num <= 8)
                {
                    upcomingRoadType = "City";

                    List<string> cityNames = new List<string>();
                    cityNames.Add("New York");
                    cityNames.Add("Las Vegas");
                    upcomingRoad = cityNames[rand.Next(0, cityNames.Count)];
                }
                else
                {
                    upcomingRoadType = "Road";
                    List<string> streetNames = new List<string>();
                    streetNames.Add("Main Street");
                    streetNames.Add("Lowel Street");
                    upcomingRoad = streetNames[rand.Next(0, streetNames.Count)];
                }
            }
        }

        public static void InitalizeCar()
        {
            int theSerial = 0;
            foreach(KeyValuePair<int,Vehicle> ownedCar in ownedVehicles)
            {
                if(ownedCar.Value.amount > 0)
                {
                    theSerial = ownedCar.Key;
                }
            }

            carName = ownedVehicles[theSerial].name;
            carAccelerateSpeed = ownedVehicles[theSerial].acceleration;
            carTopSpeed = ownedVehicles[theSerial].topSpeed;
            carBrakeSpeed = ownedVehicles[theSerial].brakeSpeed;
        }
        //Speed Controllers
        public static void SlowDown(string type)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            type = type.ToLower();
            while (stopwatch.IsRunning == true)
            {
                int speed = 0;
                if (type == "brake")
                {
                    speed = carBrakeSpeed;
                }
                else
                {
                    speed = idleSpeed;
                }

                if (stopwatch.ElapsedMilliseconds >= speed)
                {
                    if(currentSpeed > 0)
                    {
                        EditSpeed(-1);
                    }
                    stopwatch.Stop();
                }
            }
        }
        public static void SpeedUp()
        {
            Random rand = new Random();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.IsRunning == true )
            {
                if (stopwatch.ElapsedMilliseconds >= carAccelerateSpeed)
                {
                    if (currentSpeed < carTopSpeed)
                    {
                        EditSpeed(1);
                    }
                    else
                    {
                        double overSpeed = 0;
                        overSpeed = currentSpeed - carTopSpeed;
                        if (overSpeed >= 2)
                        {
                            EditSpeed(rand.Next(-2, -1));
                        }
                        else
                        {
                            int num = rand.Next(1, 10);
                            if (num == 1 || num == 2)
                            {
                                EditSpeed(1);
                            }
                        }
                    }

                    stopwatch.Stop();
                }
            }
        }
        public static void EditSpeed(int howMuch)
        {
            currentSpeed += howMuch;

            if (currentSpeed <= 0 && wantedLevel >= 1)
            {
                arrestCountdown.Enabled = true;
            }

            if (howMuch > 0)
            {
                if (arrestCountdown.Enabled == true)
                {
                    if(wantedLevel == 1)
                    {
                        wantedLevel = 2;
                    }

                    resistArrestTimer.Enabled = true;
                    arrestCountdown.Enabled = false;
                }
            }

            if (currentSpeed >= theSpeedLimit)
            {
                speeding = true;
                breakingLaw = true;
            }
            else
            {
                speeding = false;
                breakingLaw = false;
            }


            if (speedCaughtAt > 0)
            {
                if (speedCaughtAt < currentSpeed)
                {
                    speedCaughtAt = currentSpeed;
                }
            }
        }



        //Penalty Stuff
        public static void GetPulledOver()
        {

            if (wantedLevel > 1)
            {
                isArrested = true;
            }

            if (isArrested != true)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Random rand = new Random();
                Stopwatch wait = new Stopwatch();
                wait.Start();
                Console.WriteLine("An officer is approaching your window...");
                while(wait.ElapsedMilliseconds <= 2500)
                { }

                if (speedCaughtAt > 0)
                {
                    Console.Clear();
                    List<string> speedingMessages = new List<string>();
                    //speedingMessages.Add("\"Do you know how fast you were goin?\"");
                    speedingMessages.Add("\"Boi... do you know I caught you at " + speedCaughtAt + " mph?\"");
                    string speedingMessage = speedingMessages[rand.Next(0, speedingMessages.Count)];

                    Options(speedingMessage, "Yes sir. " + speedCaughtAt + " miles per hour.", "Uh no, I wasn't really paying attention.", "Fuck off pig");
                    if (answer == "1")
                    {
                        int num = rand.Next(1, 10);
                        if (num >= 1 && num <= 5)
                        {
                            Console.WriteLine("Eh you're a good kid. Ill let you go!");
                            Console.ReadLine();
                        }
                        else
                        {
                            CollectFine("Speeding");
                        }

                        wantedLevel = 0;
                    }
                    else if (answer == "2")
                    {

                    }
                    else if (answer == "3")
                    {
                        isArrested = true;
                    }

                    speedCaughtAt = 0;
                }
            }
        }
        public static void GetArrested()
        {
            Random rand = new Random();
            Console.Clear();
            List<string> arrestedWays = new List<string>();

            if (wantedLevel == 1)
            {
                arrestedWays.Add("You were taken out of the car and cuffed!");
                arrestedWays.Add("You were thrown on the ground and clubbed!");
            }
            else if (wantedLevel == 2)
            {
                arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
                arrestedWays.Add("You were beaten!");
            }
            else if (wantedLevel == 3)
            {
                arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
                arrestedWays.Add("You were beaten!");
            }
            else if (wantedLevel == 4)
            {
                arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
                arrestedWays.Add("You were beaten!");
            }
            else if (wantedLevel == 5)
            {
                arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
                arrestedWays.Add("You were beaten!");
            }
            else
            {
                arrestedWays.Add("Cops swarm the car and tell you to put your hands on the wheel. You're arrested!");
                arrestedWays.Add("You were beaten!");
            }

            Console.WriteLine(arrestedWays[rand.Next(0, arrestedWays.Count)]);
            Console.ReadLine();
            wantedLevel = 0;
            speedCaughtAt = 0;
            isDriving = false;
        }
        public static void CollectFine(string what)
        {
            what = what.ToLower();
            if (what == "speeding")
            {
                Console.WriteLine("You were issued a fine for speeding!");
            }
            else if (what == "resisting arrest")
            {
                Console.WriteLine("You were issued a fine for resisting arrest!");
            }
            else
            {

            }

            Console.ReadKey();
        }


        public static void EscapeCops()
        {
            wantedLevel = 0;
            speedCaughtAt = 0;
            resistArrestTimer.Enabled = false;
        }



        //Timer Controllers
        private static void CorrectDistance(object source, System.Timers.ElapsedEventArgs e)
        {
            distanceLeft -= ((currentSpeed * 5280) / 36000);
            drivingMoney += ((currentSpeed) / 25);

            if (distanceLeft <= 0)
            {
                GetNewRoad();
            }
        }
        private static void PoliceSeeYou(object source, System.Timers.ElapsedEventArgs e)
        {
            Random rand = new Random();
            if (breakingLaw == true && policeSeeYouTimer.Enabled == true)
            {
                if (speeding == true)
                {
                    speedCaughtAt = currentSpeed;
                    speedLimitCaughtAt = theSpeedLimit;
                }

                policeSeeYouTimer.Enabled = false;
                policeWillAppear.Stop();
                resistArrestTimer.Enabled = true;
                wantedLevel = 1;
            }
            else
            {
                if(policeSeeYouTimer.Enabled == false)
                {
                    policeSeeYouTimer.Enabled = true;
                }

                policeSeeYouTimer.Interval = rand.Next(4000, 5000);
                policeWillAppear.Restart();
            }
        }
        private static void ResistArrest(object source, System.Timers.ElapsedEventArgs e)
        {
            if (resistArrestTimer.Interval == resistArrestBaseTime)
            {
                checkSpeed = currentSpeed;
                resistArrestTimer.Interval = 1000;
            }
            else
            {
                if (currentSpeed >= checkSpeed)
                {
                    if (wantedLevel == 1)
                    {
                        wantedLevel = 2;
                        resistArrestTimer.Interval = 3000;
                    }
                    else if (wantedLevel == 2)
                    {
                        wantedLevel = 3;
                        resistArrestTimer.Interval = 3000;
                    }
                    else if (wantedLevel == 3)
                    {
                        wantedLevel = 4;
                        resistArrestTimer.Interval = 3000;
                    }
                    else
                    {
                        wantedLevel = 5;
                        resistArrestTimer.Interval = 3000;
                    }
                }
                else
                {
                    resistArrestTimer.Interval = resistArrestBaseTime;
                }
            }
        }
        public static void SayConvo(object source, System.Timers.ElapsedEventArgs e)
        {
            Random rand = new Random();
            if (convoTimer.Interval == convoBaseTime)
            {
                List<string> pullOverConvos = new List<string>();
                pullOverConvos.Add("Pull your car over!");
                pullOverConvos.Add("Pull over!");
                convoText = "\"" + pullOverConvos[rand.Next(0, pullOverConvos.Count)] + "\"";
                convoTimer.Interval = 2000;
            }
            else
            {
                convoText = "";
                convoTimer.Interval = convoBaseTime;
            }
        }
        public static void ConfrontPolice(object source, System.Timers.ElapsedEventArgs e)
        {
            isDriving = false;
            arrestCountdown.Enabled = false;
            resistArrestTimer.Enabled = false;
        }
        public static void ControlTimers(string pauseOrResume)
        {
            pauseOrResume = pauseOrResume.ToLower();
            if (pauseOrResume == "pause")
            {
                getDistanceTimer.Enabled = false;
                policeSeeYouTimer.Enabled = false;
                resistArrestTimer.Enabled = false;
            }
            else
            {
                getDistanceTimer.Enabled = true;
                policeSeeYouTimer.Enabled = true;
            }
        }


        //Message Controllers
        public static void Options(string message, params string[] options)
        {
            int whileLoopNum = 1;
            while (whileLoopNum == 1)
            {
                Dictionary<int, string> acceptableOptions = new Dictionary<int, string>();
                List<string> optionsList = new List<string>();
                int number = 1;
                string availableOptions = "";

                foreach (string option in options)
                {
                    optionsList.Add(option);
                }

                foreach (string option in optionsList)
                {
                    if (option != "")
                    {
                        acceptableOptions.Add(number, option);
                    }

                    number++;
                }

                foreach (KeyValuePair<int, string> acceptableOp in acceptableOptions)
                {
                    availableOptions += ("[" + acceptableOp.Key + "] " + acceptableOp.Value + "\n");
                }

                Console.WriteLine(CorrectMessage(message) + "\n\n\n" + availableOptions);


                answer = Console.ReadLine();
                bool parsedWorked = int.TryParse(answer, out number);

                if (parsedWorked)
                {
                    if (acceptableOptions.ContainsKey(number))
                    {
                        whileLoopNum = 2;
                    }
                    else
                    {
                        WrongResponse();
                    }
                }
                else
                {
                    WrongResponse();
                }
            }

            Console.Clear();
        }
        public static string CorrectMessage(string message)
        {
            int spacing = 100;

            for (int i = 0; i < message.Length; i++)
            {
                char character = message[i];
                if (char.IsWhiteSpace(character) && i >= spacing)
                {
                    message = message.Insert(i + 1, "\n");
                    spacing += spacing;
                }
            }

            return message;
        }
        public static void WrongResponse()
        {
            Console.Clear();
            string response;
            Random rnd = new Random();
            List<string> responses = new List<string>();

            responses.Add("Oops. Wrong Response.");
            responses.Add("Wrong Key Pressed.");
            responses.Add("Unknown Command. Try Again.");
            responses.Add("Invalid answer.");
            responses.Add("Invalid input!");
            responses.Add("Unknown Key.");
            responses.Add("Wrong answer!");
            responses.Add("Incorrect Key.");

            response = responses[rnd.Next(responses.Count)];
            Console.WriteLine(response);
            Console.ReadKey();
            Console.Clear();

        }


        public static int GrabSerial()
        {
            Random rand = new Random();
            int serial = rand.Next(-999999, 9999999);
            while(serials.Contains(serial))
            {
                serial = rand.Next(-999999, 9999999);
            }

            return serial;
        }
    }
}
