using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TodoApp.Api.Middlewares.ErrorHandling;
using TodoApp.Bll.PipelineBehaviors;
using TodoApp.Dal;
using TodoApp.Dal.Repositories;
using TodoApp.Dal.UoW;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.UoW;

namespace TodoApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoAppDbContext>(opt =>
            {
                opt.UseSqlite(Configuration.GetConnectionString("TodoAppDbContext"));
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            }));

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>(sp => new EfCoreUnitOfWork(sp.GetRequiredService<TodoAppDbContext>()));

            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddMediatR(Assembly.Load("TodoApp.Bll"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));

            services.AddControllers()
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssembly(Assembly.Load("TodoApp.Bll"));
                });

            services
                .AddOpenApiDocument(d =>
                {
                    d.DocumentName = "TodoApp API";
                    d.Title = d.DocumentName;
                    d.UseRouteNameAsOperationId = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
