using Clearfield.Application;
using FluentMigrator.Runner;
using Homuai.Api.Configuration.Swagger;
using Homuai.Api.Filter;
using Homuai.Api.Filter.Authentication;
using Homuai.Api.Middleware;
using Homuai.Api.Services;
using Homuai.Api.WebSocket.AddFriend;
using Homuai.EmailHelper;
using Homuai.Infrastructure;
using Homuai.Infrastructure.DataAccess;
using Homuai.Infrastructure.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Reflection;

#pragma warning disable ASP0000
namespace Homuai.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddHashids(setup =>
            {
                setup.Salt = Configuration.GetValue<string>("Settings:IdCryptographySalt");
                setup.MinHashLength = 3;
            });

            services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Application.Services.AutoMapper.AutoMapping(provider.GetService<HashidsNet.IHashids>()));
            }).CreateMapper());

            services.AddSignalR();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services.AddSwaggerGen(options => {

                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                for (var indice = provider.ApiVersionDescriptions.Count - 1; indice >= 0; indice--)
                {
                    var description = provider.ApiVersionDescriptions.ElementAt(indice);
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"{this.GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                        Description = description.IsDeprecated ? $"{GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
                    });
                }

                options.SchemaFilter<SwaggerSubtypeOfAttributeFilter>();
                options.SchemaFilter<EnumSchemaFilter>();
                options.IncludeXmlComments("Homuai.Api.xml");
                options.OperationFilter<HashidsOperationFilter>();
            });

            services
                .AddUseCases(Configuration)
                .AddRepositories(Configuration)
                .AddEmailHelper();

            services.AddHttpContextAccessor();

            services.AddHostedService<RunAtMidnightEveryDay>();

            services.AddScoped<AuthenticationUserAttribute>();
            services.AddScoped<AuthenticationUserIsPartOfHomeAttribute>();
            services.AddScoped<AuthenticationUserIsAdminAttribute>();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddMySql5()
                    .WithGlobalConnectionString($"{Configuration.GetConnectionString("Connection")}Database={Configuration.GetConnectionString("DatabaseName")};")
                    .ScanIn(Assembly.Load("Homuai.Infrastructure")).For.All());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<CultureMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", "Homuai.Api - " + description.GroupName.ToUpperInvariant());
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AddFriendHub>("/addNewFriend");
            });

            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<HomuaiContext>();

            if (!context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                Database.EnsureDatabase(Configuration.GetConnectionString("Connection"), Configuration.GetConnectionString("DatabaseName"));

                app.Migrate();
            }
        }
    }
}
