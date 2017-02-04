using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Service;
using Dork.Service.Default.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dork.Core.Domain;
using Dork.Core.Dal;
using Dork.Dal.Mongo.Impl;
using Dork.Web.Formatters;
using Dork.Service.Moc.Impl;
using Autofac;
using Microsoft.Extensions.DependencyModel;

namespace Dork.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new[] { new KeyValuePair<string, string>("Application:Path", env.ContentRootPath) })
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc(options =>
            {
                options.InputFormatters.Insert(0,new JilInputFormatter());
                options.OutputFormatters.Insert(0,new JilOutputFormatter());
            });

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen();

            services.AddSingleton(Configuration);
            /*services.AddTransient<IEntityService<User>, EntityService<User>>();
            services.AddTransient<IEntityService<Profile>, EntityService<Profile>>();
            services.AddTransient<IAuthService, AuthServiceMoc>();
            services.AddTransient<IMessageService, MessageServiceMoc>();
            services.AddTransient<IProfileService, ProfileServiceMoc>();*/
            AutomaticRegistrar.RegisterServices(services);

            // initialize repositories
            /*
            var connectionString = 
                $"{Configuration["MongoConfiguration:Server"]}/{Configuration["MongoConfiguration:Database"]}";

            services.AddTransient<IRepository<User>>(x => new Repository<User>($"{connectionString}"));
            services.AddTransient<IRepository<Profile>>(x => new Repository<Profile>($"{connectionString}"));
            */
            RegisterAutoMapper(builder);

            return AutomaticRegistrar.RegisterAutofac(services, builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Chat}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Chat", action = "Index" });
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            AutoMapperConfiguration.Configure(builder, DependencyContext.Default);
        }
    }
}
