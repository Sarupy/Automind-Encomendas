using Automind_Encomendas.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Automind_Encomendas.Services
{
    //Classe para simular operações de banco de dados
    public interface IPackageService
    {
        public void AddSingle(Package package);
        public List<Package> List();
        public string GetFilePath();
        public void Clear();
        public void DeleteById(string id);

    }
    public class PackageService : IPackageService
    {
        private readonly IWebHostEnvironment _env;
        public PackageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void AddSingle(Package package)
        {
            List<Package> packages = List();
            packages.Add(package);
            JsonUtil.WriteToJsonFile(GetFilePath(), packages);
        }

        public List<Package> List()
        {
            List <Package> packages = JsonUtil.ReadFromJsonFile<List<Package>>(GetFilePath());
            if (packages == null) packages = new List<Package>();
            return packages;
        }

        public void Clear()
        {
           JsonUtil.WriteToJsonFile(GetFilePath(), new List<Package>());
        }

        public void DeleteById(string id)
        {
            List<Package> packages = List();
            Package removedPackage = null;
            foreach (var item in packages)
            {
                if (item.Id.ToString() == id)
                    removedPackage = item;
            }
            packages.Remove(removedPackage);
            JsonUtil.WriteToJsonFile(GetFilePath(), packages);
        }

        public string GetFilePath()
        {
            return Path.Combine(_env.ContentRootPath, "DatabaseMock", "PackageList.json");
        }
    }
}
