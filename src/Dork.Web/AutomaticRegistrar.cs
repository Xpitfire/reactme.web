using Dork.Core.Service;
using Dork.Service.Default.Impl;
using Dork.Web.Controllers.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dork.Dal.Mongo.Impl;
using Dork.Core.Dal;
using Dork.Web.Controllers;

namespace Dork.Web
{
    public static class AutomaticRegistrar
    {
        public static void RegisterServices(IServiceCollection services)
        {
            var fullname = typeof(MessageService).AssemblyQualifiedName;
            var name = typeof(MessageService).FullName + ", ";
            var serviceAssembly = new AssemblyName(GetCleanAssemblyName(fullname, name));

            var toRegister = from s in Assembly.Load(serviceAssembly).GetExportedTypes()
                             where s.GetTypeInfo().GetCustomAttribute<ServiceAttribute>() != null
                             select s;
            foreach (var service in toRegister)
            {
                var serviceInterface = service.GetTypeInfo().GetInterface($"I{service.Name}");
                if (serviceInterface != null)
                    services.AddTransient(serviceInterface, service);
            }
        }

        public static void RegisterController(IServiceCollection services)
        {
            var fullname = typeof(MessageController).AssemblyQualifiedName;
            var name = typeof(MessageController).FullName + ", ";
            var controllerAssembly = new AssemblyName(GetCleanAssemblyName(fullname, name));
            
            var toRegister = from c in Assembly.Load(controllerAssembly).GetExportedTypes()
                             where c.GetTypeInfo().GetCustomAttribute<ControllerAttribute>() != null
                             select c;
            foreach(var controller in toRegister)
            {
                services.AddTransient(controller);
            }
        }


        public static IServiceProvider RegisterAutofac(IServiceCollection services, ContainerBuilder builder)
        {
            // Register data access
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(EntityService<>)).As(typeof(IEntityService<>)).InstancePerDependency();

            // Add services
            builder.Populate(services);

            // Create container and return the service provider
            var container = builder.Build();
            ServiceLocator.SetContainer(container);

            return container.Resolve<IServiceProvider>();
        }

        private static string GetCleanAssemblyName(string fullName, string toDelete)
        {
            int index = fullName.IndexOf(toDelete);
            return (index < 0)
                ? fullName
                : fullName.Remove(index, toDelete.Length);
        }

    }
}
