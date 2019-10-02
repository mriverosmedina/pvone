using pvone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pvone.Backend.Models
{
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<pvone.Common.Models.Product> Products { get; set; }
    }
}