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

namespace Demo1.MVC.Data
{
    public class Startup
    {
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

        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            Server=127.0.0.1;
            Port=5432;
            Database=myDataBase;
            Userid=myUsername;
            Password=myPassword;
            Protocol=3;
            SSL=true;
            SslMode=Require;
            
            postgres://amwmixukoulpps:bYko8sAw8CPoSfPME8GX6_EGf5@ec2-107-22-184-127.compute-1.amazonaws.com:5432/db0sfba6gebbvi?ssl=true
            
            
            Uri myUri = new Uri("http://server:8080/func2/SubFunc2?query=somevalue");

            // Get host part (host name or address and port). Returns "server:8080".
            string hostpart = myUri.Authority;

            // Get path and query string parts. Returns "/func2/SubFunc2?query=somevalue".
            string pathpart = myUri.PathAndQuery;

            // Get path components. Trailing separators. Returns { "/", "func2/", "sunFunc2" }.
            string[] pathsegments = myUri.Segments;

            // Get query string. Returns "?query=somevalue".
            string querystring = myUri.Query;            
            
            */
            Uri postgresUrl = new Uri(Configuration["DATABASE_URL"]);
            var server = postgresUrl.Host;
            var port = postgresUrl.Port;
            var userInfo = postgresUrl.UserInfo.Split(new char[] {':'});
            var database = postgresUrl.Segments[1];
            var hasSsl = !string.IsNullOrEmpty(postgresUrl.Query);
            
            var connString = new StringBuilder();
            connString.Append("Server=").Append(server).Append(";");
            connString.Append("Port=").Append(port).Append(";");
            connString.Append("Database=").Append(database).Append(";");
            if(userInfo.Any()) {
                connString.Append("Userid=").Append(userInfo[0]).Append(";");
                string pwd = userInfo.Length == 2? userInfo[1] : string.Empty;
                connString.Append("Password=").Append(pwd).Append(";");
            }
            // if (hasSsl) {
            //     // connString.Append("Protocol=3;");
            //     // connString.Append("SSL=true;");
            //     connString.Append("SslMode=Require;");
            //     connString.Append("Use SSL Stream=true;");
            // }
            
            // Add framework services.
            services.AddEntityFramework()
                .AddNpgsql()
                .AddDbContext<ApplicationDbContext>(options => 
                    options.UseNpgsql(connString.ToString()));
            // services.AddEntityFramework()
            //     .AddSqlite()
            //     .AddDbContext<ApplicationDbContext>(options =>
            //         options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                            .Database.Migrate();
                }
            }
            catch { }
            

        }
    }
}
