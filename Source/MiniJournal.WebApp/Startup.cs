using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PostgreSql.NHibernate;
using Infotecs.MiniJournal.RabbitMqPublisher;
using Infotecs.MiniJournal.WebApp.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infotecs.MiniJournal.WebApp
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceProvider"/>.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "ClientApp/dist";
            });

            services.AddSignalR();

            services.AddHostedService<HostedServices.MessageBusListener>();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule(new DiskStorageModule(this.configuration["ImagesStoragePath"]));
            builder.RegisterModule(new NHibernateModule(this.configuration.GetConnectionString("Postgres")));
            builder.RegisterModule(new RabbitMqModule(this.configuration.GetConnectionString("RabbitMq")));

            return new AutofacServiceProvider(builder.Build());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <param name="env"><see cref="IHostingEnvironment"/>.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationsHub>("/hubs/notifications");
            });

            app.UseMvc();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
