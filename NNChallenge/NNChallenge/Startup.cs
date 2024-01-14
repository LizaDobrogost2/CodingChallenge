using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NNChallenge.Interfaces;
using NNChallenge.Service;

namespace NNChallenge
{
    public class Startup
    {
        private static IServiceProvider _serviceProvider;

        public static void ConfigureServices(Action<IServiceCollection> configureServices)
        {
            var services = new ServiceCollection();
            configureServices?.Invoke(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
