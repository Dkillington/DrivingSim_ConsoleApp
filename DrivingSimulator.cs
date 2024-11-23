using DrivingSimulator.Functionality;
using System;

namespace DrivingSimulator
{
    public class DrivingSimulator
    {
        // Entry method
        [STAThread]
        static void Main(string[] args)
        {
            PlayGame();
            Exit();
        }

        private static void PlayGame()
        {
            Game.Instance.Start();
        }
        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
