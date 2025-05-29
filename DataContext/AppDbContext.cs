using billing_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace billing_backend.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CustomerMaster> CustomerMaster { get; set; }
        public DbSet<BillMaster> BillMaster { get; set; }
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<OtpMaster> OtpMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
    }
}
