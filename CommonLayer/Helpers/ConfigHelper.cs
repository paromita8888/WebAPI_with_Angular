using System.Configuration;

namespace WebAPI_with_Angular.Common
{
    public class ConfigHelper
    {
        public static string GetConfigValue(string configKey)
        {
            return ConfigurationManager.AppSettings[configKey];
        }
    }
}