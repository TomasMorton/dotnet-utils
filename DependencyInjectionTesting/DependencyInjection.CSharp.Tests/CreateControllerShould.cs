using System;
using System.Collections.Generic;
using System.Linq;
using Application.Api;
using Application.Api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace DependencyInjection.CSharp.Tests
{
    public class CreateControllerShould
    {
        private readonly IServiceCollection _services;

        public CreateControllerShould()
        {
            _services = RegisterServices();
        }

        [Theory]
        [MemberData(nameof(Controllers))]
        public void SuccessfullyLoadDependencies(Type controllerType)
        {
            _services.AddTransient(controllerType);
            var provider = _services.BuildServiceProvider();

            var controller = provider.GetRequiredService(controllerType);

            Assert.NotNull(controller);
        }

        public static IEnumerable<object[]> Controllers()
        {
            return ControllerFinder
                .FindControllers(typeof(BaseController))
                .Select(x => new[] {x});
        }

        private static IServiceCollection RegisterServices()
        {
            var config = CreateConfiguration();
            var startup = new Startup(config);

            return CreateServiceCollection(startup);
        }

        private static IConfigurationRoot CreateConfiguration()
        {
            var configValues = new[]
            {
                new KeyValuePair<string, string>("Config:Key", "null")
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();
        }

        private static ServiceCollection CreateServiceCollection(Startup startup)
        {
            var services = new ServiceCollection();

            //Add services provided by default
            services.AddLogging(x => x.AddConsole());
            //Add application specific services
            startup.ConfigureServices(services);

            return services;
        }
    }
}