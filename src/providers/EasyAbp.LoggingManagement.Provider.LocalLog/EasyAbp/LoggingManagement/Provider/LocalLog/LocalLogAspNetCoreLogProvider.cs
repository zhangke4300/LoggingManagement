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
using Volo.Abp.Json;
using Volo.Abp.Settings;

namespace EasyAbp.LoggingManagement.Provider.LocalLog
{
    public class LocalLogAspNetCoreLogProvider : IAspNetCoreLogProvider
    {
       
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IJsonSerializer _jsonSerializer;

        public LocalLogAspNetCoreLogProvider(
            IJsonSerializer jsonSerializer,
            IAuditLogRepository auditLogRepository)
        {
            _jsonSerializer = jsonSerializer;
            _auditLogRepository = auditLogRepository;
        }
        
        public virtual async Task<PagedResultDto<SystemLogDto>> GetListAsync(string queryString, DateTime startTime,
            DateTime endTime, int maxResultCount, int skipCount)
        {
            var count = await _auditLogRepository.GetCountAsync(queryString, startTime: startTime, endTime, maxResultCount, skipCount); 
            var audits = await _auditLogRepository.GetListAsync(queryString, startTime:startTime, endTime, maxResultCount, skipCount);
            var systemLogs = new List<SystemLogDto>();
            foreach(var audit in audits)
            {
                systemLogs.Add(new SystemLogDto()
                {
                    Level = audit.Exceptions,
                    LogName = audit.Url,
                    LogValue = _jsonSerializer.Serialize(audit),
                    Time = audit.ExecutionTime
                });
            }
            return new PagedResultDto<SystemLogDto>(count, systemLogs);
        }
    }
}