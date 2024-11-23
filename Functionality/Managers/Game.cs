using DrivingSimulator.Data;
using DrivingSimulator.Functionality;
using System;
using System.Collections.Generic;

namespace DrivingSimulator
{
    internal class Game
    {
        // Global & Belongs to Class
        private static Game instance;
        public static Game Instance => instance ??= new();

        // Belongs to specific object
        public readonly DeltaTime dt = new();
        public readonly TextFunctionality tf = new();
        public readonly GameSaveManager gsm = new();

        private bool playingGame = true;
        private DrivingManager drivingManager;

        // Constructor
        public Game()
        {
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
            gsm.NewGame();
        }
    }
}
