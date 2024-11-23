using DrivinSim.Functionality;
using System;

namespace DrivinSim
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

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
