using System.Data.Entity;

namespace pvone.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext():base("DefaultConnection")
        {



        }

        public System.Data.Entity.DbSet<pvone.Common.Models.Product> Products { get; set; }
    }
}
