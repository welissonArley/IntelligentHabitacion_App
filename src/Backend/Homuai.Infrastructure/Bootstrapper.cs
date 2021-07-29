using Homuai.Domain.Repository;
using Homuai.Domain.Repository.CleaningSchedule;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.Home;
using Homuai.Domain.Repository.MyFoods;
using Homuai.Domain.Repository.Token;
using Homuai.Domain.Repository.User;
using Homuai.Domain.Services;
using Homuai.EmailHelper.Services.SendEmail;
using Homuai.Infrastructure.DataAccess;
using Homuai.Infrastructure.DataAccess.Repositories;
using Homuai.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Homuai.Infrastructure
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISendEmail, SendGridService>(options =>
            {
                return new SendGridService(new Domain.ValueObjects.EmailConfig
                {
                    ApiKey = configuration.GetValue<string>("Settings:SendEmailSettings:ApiKey"),
                    Name = "Homuai Team",
                    Email = configuration.GetValue<string>("Settings:SendEmailSettings:FromEmail"),
                    SupportEmail = configuration.GetValue<string>("Settings:SendEmailSettings:SupportEmail")
                });
            });
            services.AddScoped<IPushNotificationService, OneSignalService>(options =>
            {
                return new OneSignalService(new Domain.ValueObjects.OneSignalConfig
                {
                    AppId = configuration.GetValue<string>("Settings:OneSignal:AppId"),
                    Url = configuration.GetValue<string>("Settings:OneSignal:Url"),
                    Key = configuration.GetValue<string>("Settings:OneSignal:Key")
                });
            });

            return services
                .AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>()
                .AddScoped<ITokenWriteOnlyRepository, TokenRepository>()
                .AddScoped<ITokenReadOnlyRepository, TokenRepository>()
                .AddScoped<ICodeReadOnlyRepository, CodeRepository>()
                .AddScoped<ICodeWriteOnlyRepository, CodeRepository>()
                .AddScoped<IHomeWriteOnlyRepository, HomeRepository>()
                .AddScoped<IHomeUpdateOnlyRepository, HomeRepository>()
                .AddScoped<IMyFoodsReadOnlyRepository, MyFoodsRepository>()
                .AddScoped<IMyFoodsUpdateOnlyRepository, MyFoodsRepository>()
                .AddScoped<IMyFoodsWriteOnlyRepository, MyFoodsRepository>()
                .AddScoped<ICleaningScheduleReadOnlyRepository, CleaningScheduleRepository>()
                .AddScoped<ICleaningScheduleWriteOnlyRepository, CleaningScheduleRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var inMemoryTests = configuration.GetValue<bool>("Settings:InMemoryTests");

            if (!inMemoryTests)
                services.AddDbContext<HomuaiContext>(options => options.UseMySql($"{configuration.GetConnectionString("Connection")}Database={configuration.GetConnectionString("DatabaseName")};"));
        }
    }
}
