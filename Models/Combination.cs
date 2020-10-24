using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automind_Encomendas.Models
{
    public class Combination
    {
        public List<Package> Packages { get; set; }
        public double FreightRate { get; set; }
        public double Volume { get; set; }

        public Combination()
        {
            this.Packages = new List<Package>();
        }

        public Combination(List<string> ids, List<Package> packages)
        {          
            this.Packages = packages.Where(x => ids.Contains(x.Id.ToString())).ToList();
            this.FreightRate = packages.Sum(x => x.FreightRate);
            this.Volume = this.Packages.Sum(x => x.Volume);
        }

        public Combination(List<Package> packages)
        {
            this.Packages = packages;
            this.FreightRate = packages.Sum(x => x.FreightRate);
            this.Volume = packages.Sum(x => x.Volume);
        }
    }
}
