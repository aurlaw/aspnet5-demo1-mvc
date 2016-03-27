using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Demo1.MVC.Services;
using Demo1.MVC.Data;

namespace Demo1.MVC
{
    public class Startup
    {
        private Demo1.MVC.Data.Startup _dataStartup;
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // // Set up configuration sources.
            // var builder = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            // if (env.IsDevelopment())
            // {
            //     // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //     builder.AddUserSecrets();
            // }

            // builder.AddEnvironmentVariables();
            // Configuration = builder.Build();
            Configuration = AppConfig.Build(env);
            
            //             Uri postgresUrl = new Uri(Configuration["DATABASE_URL"]);
            // var server = postgresUrl.Host;
            // var port = postgresUrl.Port;
            // var userInfo = postgresUrl.UserInfo.Split(new char[] {':'});
            // var database = postgresUrl.Segments[1];
            // var hasSsl = !string.IsNullOrEmpty(postgresUrl.Query);
            
            // var connString = new StringBuilder();
            // connString.Append("Server=").Append(server).Append(";");
            // connString.Append("Port=").Append(port).Append(";");
            // connString.Append("Database=").Append(database).Append(";");
            // connString.Append("Userid=").Append(userInfo[0]).Append(";");
            // connString.Append("Password=").Append(userInfo[1]).Append(";");
            // if (hasSsl) {
            //     // connString.Append("Protocol=3;");
            //     connString.Append("SSL=true;");
            //     connString.Append("SslMode=Require;");
            // }

            
            // var loggerFactory = new LoggerFactory().AddConsole();
            // var logger = loggerFactory.CreateLogger(typeof(Program).FullName);
            // logger.LogInformation("DATABASE_URL: {0}", Configuration["DATABASE_URL"]);
            // logger.LogInformation("ConnectionString: {0}", connString);
            
            
            _dataStartup = new Demo1.MVC.Data.Startup(env, appEnv);
            // Configuration["Data:DefaultConnection:ConnectionString"] = $@"Data Source={appEnv.ApplicationBasePath}/Demo1.MVC.db";

        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _dataStartup.ConfigureServices(services);
            // // Add framework services.
            // services.AddEntityFramework()
            //     .AddSqlite()
            //     .AddDbContext<ApplicationDbContext>(options =>
            //         options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]));

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>()
            //     .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
                
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                
                _dataStartup.Configure(app);

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                _dataStartup.Configure(app);
                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                // try
                // {
                //     using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                //         .CreateScope())
                //     {
                //         serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                //              .Database.Migrate();
                //     }
                // }
                // catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
