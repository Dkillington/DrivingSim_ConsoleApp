using DrivinSim.Functionality;
using System;

namespace DrivinSim
{
    public class DrivingSimulator
    {
        [STAThread]
        static void Main(string[] args)
        {
            PlayGame();
            Exit();
        }

        static void PlayGame()
        {
            Game.Instance.Start();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
