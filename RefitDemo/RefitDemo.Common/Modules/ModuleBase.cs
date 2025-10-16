using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefitDemo.Common.Modules
{
    public abstract class ModuleBase
    {
        public virtual void ConfigureServices(IServiceCollection services) { }

        // Optional: Called after all modules are initialized
        public virtual void OnInitialized(IServiceProvider serviceProvider) { }

        public static void InitializeModules(IServiceCollection services, IEnumerable<Type> moduleTypes)
        {
            var initializedModules = new HashSet<Type>();

            foreach (var moduleType in moduleTypes)
            {
                InitializeModule(services, moduleType, initializedModules);
            }

            // Create a service provider to call OnInitialized
            var serviceProvider = services.BuildServiceProvider();
            foreach (var moduleType in initializedModules)
            {
                var moduleInstance = (ModuleBase)Activator.CreateInstance(moduleType);
                moduleInstance.OnInitialized(serviceProvider);
            }
        }

        private static void InitializeModule(IServiceCollection services, Type moduleType, HashSet<Type> initializedModules)
        {
            if (initializedModules.Contains(moduleType))
                return;

            try
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true)
                    .Cast<DependsOnAttribute>();

                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependency in dependsOnAttribute.DependedModuleTypes)
                    {
                        InitializeModule(services, dependency, initializedModules);
                    }
                }

                var moduleInstance = (ModuleBase)Activator.CreateInstance(moduleType);
                moduleInstance.ConfigureServices(services);

                initializedModules.Add(moduleType);

                Console.WriteLine($"Module {moduleType.Name} initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize module {moduleType.Name}: {ex.Message}");
                throw;
            }
        }
    }
}