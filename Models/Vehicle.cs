using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automind_Encomendas.Models
{
    public class Vehicle
    {
        public string Model { get; set; }
        public string Plate { get; set; }
        public double Capacity { get; set; }

        public Vehicle(string model, string plate, double capacity) {
            this.Model = model;
            this.Plate = plate;
            this.Capacity = capacity;
        }

        public Vehicle()
        {      
        }
    }
}
