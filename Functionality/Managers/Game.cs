using DrivingSimulator.Data;
using System;
using System.Collections.Generic;

namespace DrivingSimulator.Functionality
{
    internal class Game
    {
        // Global & Belongs to Class
        private static Game instance;
        public static Game Instance => instance ??= new();

        // Belongs to specific object
        public readonly DeltaTime dt;
        public readonly TextFunctionality tf;
        public readonly GameSaveManager gsm;

        private bool playingGame = true;
        private DrivingManager drivingManager;

        // Constructor
        public Game()
        {
            tf = new();
            dt = new();
            gsm = new();
        }

        // Methods
        public void Start()
        {
            gsm.LoadSaves();
            NewGame();
            MainMenu();
        }

        private void MainMenu()
        {
            List<Action> Events = [Drive, Exit];

            while (playingGame)
            {
                Events[tf.Options("Pick a menu", "Drive!", "QUIT")].Invoke();
            }
        }
        private void Drive()
        {
            drivingManager = new DrivingManager(dt, tf, gsm);
            drivingManager.Drive();
        }
        private void Exit()
        {
            playingGame = false;
        }
        private void NewGame()
        {
            WelcomeBack();

            gsm.NewGame();


            void WelcomeBack()
            {
                //tf.Write("Hey!", true);
            }
        }
    }
}
