using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyTrack.Data.MsSqlServer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTrack.Data.MsSqlServer.Db
{
    public class MoneyTrackContext: IdentityDbContext<ApplicationUser>
    {
        public MoneyTrackContext(DbContextOptions<MoneyTrackContext> options) : base(options)
        { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Account>()
                .HasOne(t => t.LastTransaction)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
