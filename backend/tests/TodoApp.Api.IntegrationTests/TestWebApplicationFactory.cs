using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
                    services.AddControllers()
                        .AddApplicationPart(Assembly.Load("TodoApp.Api"))
                        .AddFluentValidation(opt =>
                        {
                            opt.RegisterValidatorsFromAssembly(Assembly.Load("TodoApp.Bll"));
                        });

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
