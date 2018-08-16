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
    }
}