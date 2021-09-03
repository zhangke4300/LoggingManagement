using EasyAbp.LoggingManagement.Provider.LocalLog.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;

namespace EasyAbp.LoggingManagement.Provider.LocalLog
{
    public class CleanLogBackgroudWorker : AsyncPeriodicBackgroundWorkerBase
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;
        public CleanLogBackgroudWorker(AbpAsyncTimer timer, 
            IServiceScopeFactory serviceScopeFactory,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant) 
            : base(timer, serviceScopeFactory)
        {
            Timer.Period = 1 * 60 * 60 * 1000;// 1 hours
            //Timer.Period = 10 * 1000;// 10 seconds
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;
        }

        protected async override Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            //Logger.LogInformation("CleanLogBackgroudWorker Starting: Delete data of serveral days before...");
            var tenants = await _tenantRepository.GetListAsync(includeDetails: true);
            foreach (var tenant in tenants)
            {
                using (_currentTenant.Change(tenant.Id))
                {
                    await DeleteLocalLog(workerContext);
                }                
            }

            //delete host
            await DeleteLocalLog(workerContext);

            //Logger.LogInformation("CleanLogBackgroudWorker Completed: Delete data of serveral days before...");
        }

        private async Task DeleteLocalLog(PeriodicBackgroundWorkerContext workerContext)
        {
            var settingProvider = workerContext
                .ServiceProvider
                .GetRequiredService<ISettingProvider>();
            if (await settingProvider.GetOrNullAsync(LoggingManagementLocalLogSettings.AutoDeleteLog) == "True")
            {
                var daysBeforeStr = await settingProvider.GetOrNullAsync(LoggingManagementLocalLogSettings.DeleteDataOfDaysBefore);
                if (int.TryParse(daysBeforeStr, out int daysBefore))
                {
                    var auditLog = workerContext
                        .ServiceProvider
                        .GetRequiredService<IAuditLogRepository>();
                    await auditLog.DeleteAsync(o => o.ExecutionTime <= DateTime.Now.AddDays(-daysBefore));
                }
            }
            Logger.LogInformation($"Successfully completed {(string.IsNullOrEmpty(_currentTenant.Name) ? "null" : _currentTenant.Name)} tenant log deletions.");
        }
    }
}
