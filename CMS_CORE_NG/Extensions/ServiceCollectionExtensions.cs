using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WritableOptionsService;

namespace CMS_CORE_NG.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureWritable<T>(this IServiceCollection services, IConfigurationSection section, string filename) where T : class, new()
        {
            services.Configure<T>(section);

            services.AddTransient<IWritableSvc<T>>(provider =>
            {
                var environment = provider.GetService<IWebHostEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableSvc<T>(environment, options, section.Key, filename);
            });
        }
    }
}
