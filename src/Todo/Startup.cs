using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Todo.DataAccess;
using Todo.Model.WebpackAssetsChildren;
using Todo.Model;

namespace Todo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            if (env == null) throw new ArgumentNullException(nameof(env));

            _env = env;

            Configuration = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile("webpack-assets.json", optional: false)
               .Build();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, SqlDbContext dbContext)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseFileServer();

            DbInitializer.Initialize(dbContext);
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
            
            services.AddDbContext<SqlDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<IDatabase, SqlDbContext>();
            services.Configure<main>(Configuration.GetSection("main"));
            services.Configure<vendor>(Configuration.GetSection("vendor"));
            services.Configure<ConfigCore>(Configuration.GetSection("Core"));

        }
    }
}