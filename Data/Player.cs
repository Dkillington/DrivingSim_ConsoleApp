using DrivinSim.Factories;
using DrivinSim.Models.Vehicles;
using System.Collections.Generic;

namespace DrivinSim.Data
{
    internal class Player
    {
        public decimal Money { get; set; } = 0m;
        public List<Vehicle> OwnedVehicles { get; set; }

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
