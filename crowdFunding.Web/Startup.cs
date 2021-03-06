using crowdFunding.Core.Data;
using crowdFunding.Core.Services;
using crowdFunding.Core.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace crowdFunding.Web
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
            string ConnectionString =
            "Server =localhost; " +
            "Database = CrowdFunding; " +
            "User Id =sa; " +
            "Password =admin!@#123;";

            services.AddDbContext<CrowdFundingDbContext>(options =>
                options.UseSqlServer(ConnectionString));
            //services.AddDbContext<CrowdFundingDbContext>(options => options.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("crowdFunding.Web")));

            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IRewardPackageService, RewardPackageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IBackedProjectsService, BackedProjectsService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IVideoService,VideoService>();
            services.AddScoped<IPostService, PostService>();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
