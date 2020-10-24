using Automind_Encomendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Automind_Encomendas.Controllers.FormController;

namespace Automind_Encomendas.ViewModels
{
    public class FormViewModel
    {
        public List<Package> Packages { get; set; }
        public Vehicle Vehicle { get; set; }
        public Combination Combination { get; set; }
        public string Message { get; set; }
        public bool EmptyPackages { get; set; }
    }
}
