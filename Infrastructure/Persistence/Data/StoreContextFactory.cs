using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Persistence.Data
{
   

    namespace Persistence.Data
    {
        public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
        {
            public StoreContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=testdb;Username=postgres;Password=1234");

                return new StoreContext(optionsBuilder.Options);
            }
        }
    }
}
