using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ToDoCDN
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
                //.SetBasePath(env.WebRootPath)
                .Build();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"dist")),
                RequestPath = new PathString("")
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

        }
    }
}
