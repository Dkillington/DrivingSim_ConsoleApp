using DrivingSimulator.Factories;
using DrivingSimulator.Models.Vehicles;
using System.Collections.Generic;

namespace DrivingSimulator.Data
{
    internal class Player
    {
        public decimal Money { get; set; } = 0m;
        public List<Vehicle> OwnedVehicles { get; set; } = new();

        public Vehicle Vehicle { get; set; }

        public Player()
        {
            Vehicle = VehicleFactory.RandomVehicle();
            OwnedVehicles.Add(Vehicle);
        }
        public string ShowMoney()
        {
            return $"${Money.ToString($"#,##0")}";
        }

    }
}
