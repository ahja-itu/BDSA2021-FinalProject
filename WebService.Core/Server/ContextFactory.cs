using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using WebService.Core.Server;
using WebService.Entities;

namespace WebService.Core.Server;
public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddUserSecrets<Program>()
        .AddJsonFile("appsettings.json")
        .Build();

        var connectionString = configuration.GetConnectionString("btg");

        var optionsBuilder = new DbContextOptionsBuilder<Context>().UseNpgsql(connectionString);

        return new Context(optionsBuilder.Options);
    }
}
