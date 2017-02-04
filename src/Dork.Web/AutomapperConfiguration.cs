using Autofac;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dork.Web
{
    public class AutoMapperConfiguration
    {
        public static void Configure(ContainerBuilder builder, DependencyContext dependencyContext)
        {
            var assemblies = dependencyContext.RuntimeLibraries
                                              .SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext)
                                                                    .Select(Assembly.Load));
            var exportedTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    foreach (var exportedType in assembly.ExportedTypes) { exportedTypes.Add(exportedType); }
                }
                catch (TypeLoadException) { }
            }

            AddAutoMapperClasses(builder, exportedTypes);
            AddCustomResolvers(builder, exportedTypes);

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }

        private static void AddAutoMapperClasses(ContainerBuilder builder, IEnumerable<Type> exportedTypes)
        {
            var profiles = exportedTypes
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                .Where(t => !t.GetTypeInfo().IsAbstract);

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));
        }

        private static void AddCustomResolvers(ContainerBuilder builder, IEnumerable<Type> exportedTypes)
        {
            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>)
            };
            foreach (var openType in openTypes)
            {
                foreach (var type in exportedTypes
                    .Where(t => t.GetTypeInfo().IsClass)
                    .Where(t => !t.GetTypeInfo().IsAbstract)
                    .Where(t => t.ImplementsGenericInterface(openType)))
                {
                    builder.RegisterGeneric(type).InstancePerLifetimeScope();
                }
            }
        }
    }
}
