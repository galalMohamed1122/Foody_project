using AutoMapper;
using Foody_project.Mapper;
using FoodyDAL.Models;
using FoodyDl.FoodyService;
using FoodyDl.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Foody_project
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
            //services.AddDbContext<FoodyContext>(opt => opt.UseInMemoryDatabase("Foody"));

            services.AddDbContext<FoodyContext>(
            options => options.UseSqlServer(Configuration.GetSection("ConnectionString").Value));

            #region Auto Mapper Configurations

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new APIAutoMapperProfile());
                mc.AddProfile(new DBAutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Dependency Injection

         
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IRestaurant, RestaurantService>();
            services.AddScoped<ICharity, CharityServices>();
            services.AddScoped<IDelivary, DeliveryServices>();

            #endregion

            #region Swagger

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = "Authentication and Authorization in ASP.NET 5 with JWT and Swagger"
                });
            });

      ������#endregion

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementMicroservice.API�v1"));

            app.UseHttpsRedirection();
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
