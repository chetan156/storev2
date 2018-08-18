using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace storev2.Models
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext():base("name=StoreCon")
        {
            Database.SetInitializer<StoreDbContext>(null);
        }
        public DbSet<CatagoryMaster> CatagoryMasters { get; set; }
        public DbSet<CompanyMaster> CompanyMasters { get; set; }

        public DbSet<AreaMaster> AreaMasters { get; set; }
        public DbSet<CustomerMaster> CustomerMaster { get; set; }
    }
}