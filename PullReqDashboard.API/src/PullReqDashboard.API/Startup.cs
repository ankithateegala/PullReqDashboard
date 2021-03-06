﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PullReqDashboard.API.Interfaces;
using PullReqDashboard.API.Utilities;

namespace PullReqDashboard.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //var settings = new JsonSerializerSettings();
            //settings.ContractResolver = new SignalRContractResolver();

            //var serializer = JsonSerializer.Create(settings);

            //services.Add(new ServiceDescriptor(typeof(JsonSerializer),
            //             provider => serializer,
            //             ServiceLifetime.Transient));

            //cors
            services.AddCors();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddRouting();
            
            services.AddTransient<IDBHelper, DBHelper>();
            //services.AddTransient<IConnectionManager, ConnectionManager>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseDeveloperExceptionPage();
            
            app.UseStaticFiles();

            app.UseCors(builder => builder
                                    .WithOrigins("http://pr-dashboard.s3-website-us-east-1.amazonaws.com")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                                    );

            app.UseWebSockets();
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRHub>("/PRDHub");
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
            
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                DefaultFilesOptions =
                {
                    DefaultFileNames = {"client.html"},
                    RequestPath = new PathString("/client")
                },
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "App"))
            };
            app.UseFileServer(options);
        }
    }
}
