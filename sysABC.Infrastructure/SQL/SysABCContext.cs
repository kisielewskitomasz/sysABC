using System;
using Microsoft.EntityFrameworkCore;
using sysABC.Core.Models;

namespace sysABC.Infrastructure.SQL
{
    public class SysABCContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SysABCContext(DbContextOptions<SysABCContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.Id);
        } 
    }
}
