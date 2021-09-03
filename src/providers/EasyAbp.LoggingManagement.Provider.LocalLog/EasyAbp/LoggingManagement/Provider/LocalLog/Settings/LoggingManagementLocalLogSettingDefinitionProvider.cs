using System;
using EasyAbp.Abp.TencentCloud.Cls;
using EasyAbp.Abp.TencentCloud.Common;
using EasyAbp.LoggingManagement.Provider.LocalLog.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.LoggingManagement.Provider.LocalLog.Settings
{
    public class LoggingManagementLocalLogSettingDefinitionProvider : SettingDefinitionProvider
    {
        public LoggingManagementLocalLogSettingDefinitionProvider()
        {
        }
        
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from WeChatPaySettings class.
             */
            context.Add(new SettingDefinition(
                LoggingManagementLocalLogSettings.AutoDeleteLog,
                "False",
                L("DisplayName:EasyAbp.LoggingManagement.Provider.LocalLog.AutoDeleteLog"),
                L("Description:EasyAbp.LoggingManagement.Provider.LocalLog.AutoDeleteLog"),
                isVisibleToClients: true)
                .WithProperty("Type", "select")
                .WithProperty("Options", "True|False")
                .WithProperty("Group1", "Log")
                .WithProperty("Group2", "AutoDeleteLog")
            );

            context.Add(new SettingDefinition(
                LoggingManagementLocalLogSettings.DeleteDataOfDaysBefore,
                "7",
                L("DisplayName:EasyAbp.LoggingManagement.Provider.LocalLog.DeleteDataOfDaysBefore"),
                L("Description:EasyAbp.LoggingManagement.Provider.LocalLog.DeleteDataOfDaysBefore"),
                isVisibleToClients: true)
                .WithProperty("Type", "number")
                .WithProperty("Group1", "Log")
                .WithProperty("Group2", "DeleteDataOfDaysBefore")
            );
        }
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LocalLogResource>(name);
        }
    }
}