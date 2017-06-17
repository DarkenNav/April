using April.DataAccess.Initializer;
using April.Domain.Entity;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.DataAccess.Context
{
    public class AprilDbContext
        : DbContext
    {
        public AprilDbContext()
            : base("AprilDbContext")
        {
            Configuration.ValidateOnSaveEnabled = false;
            Database.SetInitializer(new AprilDbContextInitializer());
        }

        static AprilDbContext()
        {
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<DataGridColumnCastomization> DataGridColumnCastomization { get; set; }
    }
}
