using System;
using System.Threading.Tasks;

namespace sysABC.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
