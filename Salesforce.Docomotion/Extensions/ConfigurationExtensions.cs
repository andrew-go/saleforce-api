using Microsoft.Extensions.Configuration;

namespace Salesforce.Docomotion.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetSection<T>(this IConfiguration configuration)
        {
            return configuration.GetSection<T>(typeof(T).Name);
        }

        public static T GetSection<T>(this IConfiguration configuration, string key)
        {
            return ConfigurationBinder.Get<T>(configuration.GetSection(key));
        }
    }
}
