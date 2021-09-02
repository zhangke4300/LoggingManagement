using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.LoggingManagement.SystemLogs;
using EasyAbp.LoggingManagement.SystemLogs.Dtos;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.LoggingManagement.Provider.TencentCloudCls
{
    public class LocalLogAspNetCoreLogProvider : IAspNetCoreLogProvider
    {
        private const int Limit = 100;
        
        private readonly ISettingProvider _settingProvider;
        private readonly ISearchLogResponseConverter _searchLogResponseConverter;
        private readonly IAuditLogRepository _auditLogRepository;

        public LocalLogAspNetCoreLogProvider(
            ISettingProvider settingProvider,
            IAuditLogRepository auditLogRepository,
            ISearchLogResponseConverter searchLogResponseConverter)
        {
            _settingProvider = settingProvider;
            _auditLogRepository = auditLogRepository;
            _searchLogResponseConverter = searchLogResponseConverter;
        }
        
        public virtual async Task<PagedResultDto<SystemLogDto>> GetListAsync(string queryString, DateTime startTime,
            DateTime endTime, int maxResultCount, int skipCount)
        {
            var result = await _auditLogRepository.GetListAsync(queryString, startTime:startTime, endTime, maxResultCount, skipCount);            
        }
    }
}