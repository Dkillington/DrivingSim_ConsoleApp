namespace DrivinSim.Models
{
    public class EngineFactory
    {
        public Engine CreateEngine(decimal acceleration, decimal deacceleration, decimal maxSpeed)
        {
            return new Engine()
            {
                Acceleration = acceleration,
                Deceleration = deacceleration,
                MaxSpeed = maxSpeed
            };
        }
    }
}
