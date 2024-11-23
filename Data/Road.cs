using System;

namespace DrivingSimulator.Data
{
    internal class Road
    {
        public string Name { get; set; } = "123 Main Street";

        public RoadType RType { get; set; } = RoadType.City;

        public int SpeedLimit { get; set; } = 25;
    }

    public enum RoadType
    {
        Residential,
        City,
        Highway,
        Offroad,
    }
}
