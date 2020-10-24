using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Automind_Encomendas.Models;
using Newtonsoft.Json;
using Automind_Encomendas.ViewModels;
using Automind_Encomendas.Services;

namespace Automind_Encomendas.Controllers
{
    public class FormController : Controller
    {
        private readonly ILogger<FormController> _logger;
        private readonly IPackageService _packageService;

        public FormController(ILogger<FormController> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }

        public IActionResult Index(List<string> ids, string model, string plate, double capacity)
        {
            List<Package> packages = _packageService.List();
            Combination combination = null;
            Vehicle vehicle = null;
            if (ids.Count>0) 
                combination = new Combination(ids, packages);
            if (capacity > 0) 
                vehicle = new Vehicle(model, plate, capacity);

            return View(new FormViewModel()
            {
                Packages = packages,
                Combination = combination,
                Vehicle = vehicle
            }); ;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult RegisterPackage(PackageInputModel inputModel)
        {
            Package package = new Package(inputModel.Name, inputModel.Volume);
            _packageService.AddSingle(package);
            return RedirectToAction("Index", "Form");
        }
        public IActionResult DeletePackage(string id)
        {
            _packageService.DeleteById(id);
            return RedirectToAction("Index", "Form");
        }

        public IActionResult CalcFreightRate(Vehicle vehicle)
        {
            List<Package> packages = _packageService.List();
            List<Combination> combinations = new List<Combination>();
            List<List<Package>> allCombos = GetAllCombos<Package>(packages);
            foreach (var packageList in allCombos)
            {
                Combination combination = new Combination(packageList);              
                combinations.Add(combination);
            }
            List<Combination> validCombinations = combinations.Where(x => x.Packages.Sum(y => y.Volume) <= vehicle.Capacity)?.ToList();
            var rightCombination = validCombinations.FirstOrDefault(x=>x.Volume == validCombinations.Max(y=>y.Volume));
            var rightCombinationIds = rightCombination?.Packages?.Select(x=>x.Id.ToString());
            return RedirectToAction("Index", "Form", new { ids = rightCombinationIds, model = vehicle.Model, plate = vehicle.Plate, capacity = vehicle.Capacity });
        }

        public class PackageInputModel
        {
            public string Name { get; set; }
            public double Volume { get; set; }
        }

        public static List<List<Package>> GetAllCombos<Package>(List<Package> list)
        {
            List<List<Package>> result = new List<List<Package>>();
            result.Add(new List<Package>());
            result.Last().Add(list[0]);
            if (list.Count == 1)
                return result;
            List<List<Package>> tailCombos = GetAllCombos(list.Skip(1).ToList());
            tailCombos.ForEach(combo =>
            {
                result.Add(new List<Package>(combo));
                combo.Add(list[0]);
                result.Add(new List<Package>(combo));
            });
            return result;
        }
    }
}
