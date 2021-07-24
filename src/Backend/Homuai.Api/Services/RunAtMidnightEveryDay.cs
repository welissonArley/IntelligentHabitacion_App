using Homuai.Application.UseCases.CleaningSchedule.ProcessRemindersOfCleaningTasks;
using Homuai.Application.UseCases.MyFoods.ProcessFoodsNextToDueDate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Homuai.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RunAtMidnightEveryDay : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly IServiceProvider _serviceProvider;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public RunAtMidnightEveryDay(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var delay = DateTime.UtcNow.Date.AddDays(1) - DateTime.UtcNow;
            _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (sender, args) =>
            {
                _timer.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    await NotifyUserProductNextToDueDate();
                    await NotifyUserReminderOfCleaningTasks();

                    await ScheduleJob(cancellationToken);
                }

            };
            _timer.Start();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task NotifyUserProductNextToDueDate()
        {
            using var scope = _serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IProcessFoodsNextToDueDateUseCase>();
            await useCase.Execute();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task NotifyUserReminderOfCleaningTasks()
        {
            using var scope = _serviceProvider.CreateScope();
            var useCase = scope.ServiceProvider.GetRequiredService<IProcessRemindersOfCleaningTasksUseCase>();
            await useCase.Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
