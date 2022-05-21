using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Mappings.AutoMapper;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Business.ValidationRules.FluentValidation;
using AdvertisementApp.DataAccess.Contexts;
using AdvertisementApp.DataAccess.UnitOfWorks;
using AdvertisementApp.Dto;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.DependencyResolves.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<AdvertisementContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DB"));
            });

            var mapperConfiguration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new ProvidedServiceProfile());
                opt.AddProfile(new AdvertisementProfile());
                opt.AddProfile(new AppUserProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();
            service.AddSingleton(mapper);

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IProvidedService_Service, ProvidedService_Service>();
            service.AddScoped<IAdvertisementService, AdvertisementService>();   
            service.AddScoped<IAppUserService, AppUserService>();
            service.AddScoped<IGenderService, GenderService>();
            service.AddScoped<IAdvertisementAppUserService, AdvertisementAppUserService>();
        }
    }
}
