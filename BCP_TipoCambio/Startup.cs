using BCP_TipoCambio.Core;
using BCP_TipoCambio.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP_TipoCambio
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("BCP_ExchangeRate"));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BCP_ExchangeRate";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.LoginPath = "/Home/Login";
            });
            services.AddCors(s =>
            {
                s.AddPolicy("AllowOrigin", opt => opt.AllowAnyOrigin());
            });
            #region Inyeccion de dependencias
            services.AddScoped<IMoneyManager, MoneyManager>();
            services.AddScoped<IExchangeRateManager, ExchangeRateManager>();
            services.AddScoped<IOperationManager, OperationManager>();
            #endregion




            //services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BCP_TipoCambio", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BCP_TipoCambio v1"));
            }
            app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
