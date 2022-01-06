using ECS_Dapper_Logger_Redis.DAL;
using ECS_Dapper_Logger_Redis.Models;
using ECS_Dapper_Logger_Redis.Repositories.Implements;
using ECS_Dapper_Logger_Redis.Repositories.Interfaces;
using ECS_Dapper_Logger_Redis.Services.Implements;
using ECS_Dapper_Logger_Redis.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace ECS_Dapper_Logger_Redis
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

            services.AddControllers();

            services.AddWkhtmltopdf("wkhtmltopdf");

            services.AddSingleton<ILoggerManager, LoggerManager>();


            //Add Dependency Injection for Repositories
            services.AddScoped<ICustomerRepo, CustomerRepo>();

            services.AddScoped<IAccountRepo, AccountRepo>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IServiceCategoryRepo, ServiceCategoryRepo>();

            services.AddScoped<IServiceRepo, ServiceRepo>();

            services.AddScoped<IRoleRepo, RoleRepo>();

            services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            services.AddScoped<IServiceCustomerRepo, ServiceCustomerRepo>();

            services.AddScoped<IEmpOfCustomerRepo, EmpOfCustomerRepo>();

            services.AddScoped<IReportRepo, ReportRepo>();

            services.AddSingleton<Connection>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Add Cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                    options => options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            //Add authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;  //Save token code in HttpContext     
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                };
            });

            //Cache Redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["RedisServer"];
            });

            //Mails
            services.AddOptions();
            var mailsettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings);

            services.AddTransient<IEmailSender, MailServices>();
            //Install Microsoft.AspNetCore.HttpOverrides
            //GET X-Forward IPAddress Of Client
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("Your IP Address"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECS_Dapper_Logger_Redis", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECS_Dapper_Logger_Redis v1"));
            }


            //GET X-Forward IPAddress Of Client
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
