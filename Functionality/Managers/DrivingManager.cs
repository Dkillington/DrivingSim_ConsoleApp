using DrivinSim.Data;
using DrivinSim.Functionality;
using System;
using System.Diagnostics;

namespace DrivinSim
{
    public class DrivingManager
    {
        private readonly DeltaTime dT;
        private readonly TextFunctionality tf;
        private readonly PoliceManager policeManager;
        private readonly GameSaveManager gsm;
        private readonly GameSave data;

        private bool IsDriving { get; set; } = true;
        public string TopText { get; set; } = string.Empty;


        // Constructor with dependency injection
        internal DrivingManager(DeltaTime dT, TextFunctionality tf, GameSaveManager gsm)
        {
            this.dT = dT;
            this.tf = tf;
            this.gsm = gsm;
            data = gsm.CurrentSave;

            policeManager = new PoliceManager();
        }

        internal void Drive()
        {
            dT.Start();
            while (IsDriving)
            {
                dT.Refresh();

                Display();
                var input = tf.GetInput();
                AnalyzeInput(input);
            }

            gsm.SaveGame();
        }

        private void Display()
        {
            tf.ResetCursor();

            Header();
            SpeedText();
            ConvoText();
            ResistingArrestText();


            void Header()
            {
                tf.Write($"Current Road: {data.Location.Road.Name, 100}\n");
                tf.Write($"Cash: {data.Player.ShowMoney(),100}\n");
                tf.Write($"Active Car: {data.Player.Vehicle.Name,100}\n");
                tf.Write($"SPEED LIMIT: {data.Location.Road.SpeedLimit,100}\n-------------------------\n");
            }
            void SpeedText()
            {
                tf.Write($"SPEED: {data.Player.Vehicle.ShowSpeed(), 100}                                                                                                                   \n");
            }
            void ConvoText()
            {
                tf.Write($"{TopText,100}\n");
            }
            void ResistingArrestText()
            {
                tf.Write($"                                                                                                                   \n");
            }
        }

        public void AnalyzeInput(ConsoleKey? input)
        {
            if (input != null)
            {
                Debug.Print(input.ToString() +"\nKEY PRESSED");
                if (input == ConsoleKey.W || input == ConsoleKey.UpArrow)
                {
                    data.Player.Vehicle.IncreaseSpeed();
                }
                else if (input == ConsoleKey.S || input == ConsoleKey.DownArrow)
                {
                    data.Player.Vehicle.DecreaseSpeed(true);
                }
                else if (input == ConsoleKey.Q)
                {
                    IsDriving = false;
                }
                else
                {
                    data.Player.Vehicle.DecreaseSpeed();
                }
            }
            else
            {
                Debug.Print("NONE");
                data.Player.Vehicle.DecreaseSpeed();
            }

            CheckConditions();
        }
        private void CheckConditions()
        {
          //  policeManager.TestIfSpotted();

            //if (data.Player.Vehicle.CurrentSpeed == 0 && policeManager.PursuitStatus == PursuitStatus.InPursuit)
            //{
            //    TopText = "Whew' fast!";
            //    policeManager.ArrestPlayer(data.Player);
            //    return false;
            //}
            if(data.Player.Vehicle != null)
            {
                TopText = "";
            }
        }
    }
}
