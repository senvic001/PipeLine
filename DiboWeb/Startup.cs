using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DiboWeb.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace DiboWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GxDbContext>(Options =>
              Options.UseMySql(Configuration.GetConnectionString("AzureMySqlConnection")));
           // Options.UseSqlServer(Configuration.GetConnectionString("AzureDbConnection")));
            //Options.UseMySQL(Configuration.GetConnectionString("AzureDbConnection")));//

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
