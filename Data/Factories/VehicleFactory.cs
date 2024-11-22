using DrivinSim.Models;
using DrivinSim.Models.Vehicles;
using System;
using System.Collections.Generic;

namespace DrivinSim.Factories
{
    public class VehicleFactory
    {
        private static Random rand = new();
        public static Vehicle CreateVehicle(string name, decimal price, Engine engine)
        {
            return new Vehicle()
            {
                Name = name,
                Price = price,
                Engine = engine
            };
        }

        public static Vehicle RandomVehicle()
        {
            List<Vehicle> vehicles = new();
            vehicles.Add(new Vehicle() { Name = "Mustang"});
            vehicles.Add(new Vehicle() { Name = "Camaro"});

           return vehicles[rand.Next(0, vehicles.Count)];
        }
    }
}
