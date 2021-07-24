using Homuai.Api;
using Homuai.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Configurations;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
            .UseEnvironment("Tests")
            .ConfigureServices(services =>
            {
                services.AddEntityFrameworkInMemoryDatabase();

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();

                services.AddDbContext<HomuaiContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                    options.UseInternalServiceProvider(provider);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HomuaiContext>();

                db.Database.EnsureCreated();

                ContextSeedInMemory.Seed(db);
            });
        }
    }
}
