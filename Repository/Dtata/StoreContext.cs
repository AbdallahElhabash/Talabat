using Core.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtata
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>Options):base(Options) { }

        public DbSet<Product>
    }
}
