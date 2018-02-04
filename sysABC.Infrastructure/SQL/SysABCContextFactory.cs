using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace sysABC.Infrastructure.SQL
{
    public class SysABCContextFactory : IDesignTimeDbContextFactory<SysABCContext>
    {
        public SysABCContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SysABCContext>();

            return new SysABCContext(builder.Options);
        }
    }
}
