using System;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TrackId.Application.Queries;
using TrackId.Data;
using TrackId.Data.Entities;
using TrackId.Infrastructure;
using TrackId.Infrastructure.Mapping;

namespace TrackId.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            services.AddSingleton(appSettings);

            services.ConfigureServices(appSettings);
            services.AddAutoMapper(typeof(TrackMapping));
            services.AddControllers();
            services.AddRouting(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });
            services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<Role>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddUserStore<UserStore<ApplicationUser, Role, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            var jwtOptions = appSettings.JwtTokenOptions;
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
                {
                    opts.RequireHttpsMetadata = false;
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidIssuer = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackId API", Version = "v1" });
            });

            // TODO: Specify CORS policy
            services.AddCors();
            services.AddMediatR(typeof(BaseQueryResponse<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackId API V1");
                });
            }

            app.UseRouting();

            // TODO: Specify CORS policy
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                );
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
