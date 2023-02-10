using Microsoft.EntityFrameworkCore;

namespace DbMigrator.DbContext
{
    public class VacationPackageContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public VacationPackageContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
