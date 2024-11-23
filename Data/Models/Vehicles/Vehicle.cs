using DrivingSimulator.Data;
using DrivingSimulator.Functionality;

namespace DrivingSimulator.Models.Vehicles
{
    public class Vehicle
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = decimal.Zero;
        public Engine Engine { get; set; } = new Engine();
        public double CurrentSpeed { get; set; } = 0;
        public double Acceleration { get; set; } = 10;
        public double Deacceleration { get; set; } = 10;

        // Constructors
        public Vehicle()
        {

        }
        public Vehicle(string name, Engine engine, double acceleration, double deacceleration) : base()
        {
            Name = name;
            Engine = engine;
            Acceleration = acceleration;
            Deacceleration = deacceleration;
        }

        // Methods
        public void IncreaseSpeed()
        {
            var deltaTime = Game.Instance.dt.Time;

            CurrentSpeed += (Acceleration * deltaTime);
        }

        public void DecreaseSpeed(bool isBraking = false)
        {
            var deltaTime = Game.Instance.dt.Time;

            var deccelerationValue = (isBraking ? 100 : Deacceleration);

            var decceleration = (deccelerationValue * deltaTime);

            var result = (CurrentSpeed - decceleration);
            if (result <= 0)
            {
                CurrentSpeed = 0;
            }
            else
            {
                CurrentSpeed -= decceleration;
            }
        }

        public string ShowSpeed()
        {
            return ($"{CurrentSpeed} mph");
        }
    }
}
