using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Automind_Encomendas.Models;

namespace Automind_Encomendas.Data
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Automind_Encomendas.Models.Package> Package { get; set; }
    }
}
