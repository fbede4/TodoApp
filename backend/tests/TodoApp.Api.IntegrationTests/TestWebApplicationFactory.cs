using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TodoApp.Dal;

namespace TodoApp.Api.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    var dbContextServiceDescriptor = services
                        .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TodoAppDbContext>));

                    services.Remove(dbContextServiceDescriptor);

                    services.AddDbContext<TodoAppDbContext>(opt =>
                    {
                        opt.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();

                    dbContext.Database.EnsureCreated();
                });
        }
    }
}
