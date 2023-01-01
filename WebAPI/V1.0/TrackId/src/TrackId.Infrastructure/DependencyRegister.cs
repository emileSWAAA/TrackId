using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackId.Business;
using TrackId.Business.Interfaces;
using TrackId.Business.Services;
using TrackId.Business.User;
using TrackId.Data;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;
using TrackId.Data.Interfaces.Repositories;
using TrackId.Data.Providers;
using TrackId.Data.Repositories;

namespace TrackId.Infrastructure
{
    public static class DependencyRegister
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, AppSettings appSettings)
        {
            if (appSettings?.JwtTokenOptions == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            services.ConfigureData(appSettings);
            services.ConfigureBusiness();
            services.ConfigureApi(appSettings.JwtTokenOptions);

            return services;
        }

        private static IServiceCollection ConfigureData(this IServiceCollection services, AppSettings appSettings)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(appSettings.ConnectionString,
            //         b => b.MigrationsAssembly("TrackId.Data")));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TrackIdDb");
            });
            services.AddScoped<ITrackRepository, TrackRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        private static IServiceCollection ConfigureBusiness(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IGenreService, GenreService>();

            return services;
        }

        private static IServiceCollection ConfigureApi(this IServiceCollection services, JwtTokenOptions tokenOptions)
        {
            services.AddScoped<IJwtTokenHelper, JwtTokenHelper>(serviceProvider =>
                new JwtTokenHelper(tokenOptions,
                    serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(),
                    serviceProvider.GetRequiredService<IMapper>(),
                    serviceProvider.GetRequiredService<IDateTimeProvider>()));

            return services;
        }
    }
}
