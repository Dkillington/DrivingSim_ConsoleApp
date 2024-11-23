using System.Diagnostics;

namespace DrivingSimulator.Data
{
    // An object for getting a DeltaTime
    // Usage:
        // Initalize DeltaTime in a class that needs to use a DeltaTime (Driving, etc). Doing this starts the timer
        // Reset the timer through each loop with 'DeltaTime.Refresh()'
        // Throughout the loop, get an accurate DeltaTime by calling 'DeltaTime.Time'
    internal class DeltaTime
    {
        private Stopwatch Timer { get; set; }
        private double CurrentDelta { get; set; } = 0;
        public double Time => CurrentDelta;
        public DeltaTime()
        {
            Timer = new Stopwatch();
        }

        public void Start()
        {
            CurrentDelta = 0;
            Timer.Start();
        }
        public void Refresh()
        {
            CurrentDelta = Timer.Elapsed.TotalMilliseconds;
            Timer.Restart();
        }
    }
}
