using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Salesforce.Docomotion.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static T GetConfigSection<T>(this IServiceProvider sp)
        {
            return sp.GetRequiredService<IConfiguration>().GetSection<T>();
        }
    }
}
