using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automind_Encomendas.Models
{
    public class Package 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Volume { get; set; }
        public double FreightRate { get; set; }

        public Package(string identifier, double cubicMeters)
        {
            this.Id = Guid.NewGuid();
            this.Name = identifier;
            this.Volume = cubicMeters;
            this.FreightRate = cubicMeters * 33.3;
        }       
    }
}
