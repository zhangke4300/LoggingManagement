namespace EasyAbp.LoggingManagement.Provider.LocalLog.Settings
{
    public static class LoggingManagementLocalLogSettings
    {
        public const string GroupName = "EasyAbp.LoggingManagement.Provider.LocalLog";

        /* Add constants for setting names. Example:
         * public const string MySettingName = GroupName + ".MySettingName";
         */
        
        public const string AutoDeleteLog = GroupName + ".AutoDeleteLog";
        public const string DeleteDataOfDaysBefore = GroupName + ".DeleteDataOfDaysBefore";
    }
}