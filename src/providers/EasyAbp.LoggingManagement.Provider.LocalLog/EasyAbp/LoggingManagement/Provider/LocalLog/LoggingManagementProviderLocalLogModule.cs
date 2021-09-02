using EasyAbp.Abp.TencentCloud.Cls;
using EasyAbp.LoggingManagement.SystemLogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.LoggingManagement.Provider.TencentCloudCls
{
    [DependsOn(
        typeof(LoggingManagementApplicationModule)
    )]
    public class LoggingManagementProviderLocalLogModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<LocalLogAspNetCoreLogProvider>();
            context.Services.AddTransient<IAspNetCoreLogProvider, LocalLogAspNetCoreLogProvider>();
        }
    }
}
