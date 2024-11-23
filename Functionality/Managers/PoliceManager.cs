using System;
using System.Diagnostics;

namespace DrivingSimulator.Data
{
    internal class PoliceManager
    {
        public WantedLevel WantedLevel { get; set; } = WantedLevel.None;
        public PursuitStatus PursuitStatus { get; set; } = PursuitStatus.None;

        public PoliceManager()
        {
        }


        // Wanted Level
        public void SetWantedLevel(WantedLevel level)
        {
            WantedLevel = level;
        }
        public void IncreaseWantedLevel()
        {
            try
            {
                WantedLevel += 1;
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }
        public void DecreaseWantedLevel()
        {
            try
            {
                WantedLevel -= 1;
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        public bool TestIfSpotted()
        {
            return false;
        }

        public void ArrestPlayer(Player player)
        {

        }

    }

    public enum WantedLevel
    {
        None,
        Low,
        Medium,
        High,
        Extreme
    }

    public enum PursuitStatus
    {
        None,
        InPursuit,
        Searching,
    }
}
