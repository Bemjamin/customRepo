using System.Data.Entity;
using DAL.Entity;

namespace DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() : base("default")
        {
            
        }

        public IDbSet<Employee> Employees { get; set; }

    }
}
